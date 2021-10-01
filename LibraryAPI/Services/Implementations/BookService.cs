using LibraryAPI.BookPricesProvider;
using LibraryAPI.Database.Mapper;
using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Enums;
using LibraryAPI.Models.DTOs;
using LibraryAPI.Models.POCOs;
using LibraryAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services.Implementations
{
    public class BookService : IBookService
    {
        private readonly IBookRepository bookRepository;
        private readonly IAuthorRepository authorRepository;
        private readonly IBookPriceProvider bookPriceProvider;
        
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository, IBookPriceProvider bookPriceProvider)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
            this.bookPriceProvider = bookPriceProvider;
        }

        public ServiceResult<bool> ChangeBookStatus(Guid bookId, Statuses status)
        {
            // Add new status to history
            StatusHistoryPOCO newHistoryStatus = new StatusHistoryPOCO
            {
                BookId = bookId,
                Status = status.ToString(),
                ModifiedDate = DateTime.Now
            };
            var response = bookRepository.InsertBookStatus(newHistoryStatus).Result;
            if (!response.IsOk()) return new ServiceResult<bool> (false, response.Code, response.Message);

            Guid newStatusId = response.Result.Id;

            // Change book current status
            var bookToChangeResponse = bookRepository.GetById(bookId);
            if (!bookToChangeResponse.IsOk()) return new ServiceResult<bool>(false, bookToChangeResponse.Code, bookToChangeResponse.Message);

            // Assign new status to book
            bookToChangeResponse.Result.CurrentStatusId = newStatusId;

            return new ServiceResult<bool>(true);
        }


        public ServiceResult<IEnumerable<Book>> GetBooksForPage(int page, int limit)
        {
            // Retrive data from repositories
            var booksResponse = bookRepository.GetAll();
            var authorsResponse = authorRepository.GetAllBookAuthors();

            if (!booksResponse.IsOk()) return new ServiceResult<IEnumerable<Book>>(null, booksResponse.Code, booksResponse.Message);
            if (!authorsResponse.IsOk()) return new ServiceResult<IEnumerable<Book>>(null, authorsResponse.Code, authorsResponse.Message);

            var authors = authorsResponse.Result.ToList();
            var books = booksResponse.Result
                .OrderBy(x => x.Title == null)
                .ThenBy(x=> x.Title)
                .Skip(page * limit)
                .Take(limit)
                .ToList();

            // Map results to Book outputs
            var bookDTOs = new List<Book>();
            foreach(var book in books)
            {
                var authorPoco = authors.Where(x => x.BookId == book.Id).FirstOrDefault()?.Author;
                var newBook = Mapper.Map(book);
                newBook.Author = Mapper.Map(authorPoco);           
                bookDTOs.Add(newBook);
            }

            return new ServiceResult<IEnumerable<Book>>(bookDTOs);
        }


        public ServiceResult<IEnumerable<BookStatus>> GetBookStatuses(Guid bookId)
        {
            // Retrive data from repositories
            var statusHistoryResponse = bookRepository.GetStatusHistoryByBookId(bookId);
            if (!statusHistoryResponse.IsOk()) return new ServiceResult<IEnumerable<BookStatus>>(null, statusHistoryResponse.Code, statusHistoryResponse.Message);

            var statusHistories = statusHistoryResponse.Result.OrderBy(x => x.ModifiedDate).ToList();

            // Map results to BookStatus outputs
            var result = Mapper.Map(statusHistories);
            return new ServiceResult<IEnumerable<BookStatus>>(result);
        }

        public ServiceResult<BookDetails> GetBookDetails(Guid bookId)
        {
            // Retrive data from repositories
            var bookResponse = bookRepository.GetById(bookId);
            if(!bookResponse.IsOk()) return new ServiceResult<BookDetails>(null, bookResponse.Code, bookResponse.Message);

            var book = bookResponse.Result;    
            var status = bookRepository.GetBookCurrentStatus(bookId).Result;

            // Get price from external API
            double? price = bookPriceProvider.GetBookPrice(bookId).Result;

            // Map results to BookDetails
            BookDetails bookDetails = Mapper.MapBookDetails(book);
            bookDetails.CurrentStatus = status;
            bookDetails.CurrentPrice = price ?? -1.0;

            var authorResponse = authorRepository.GetAuthorOfBook(bookId);
            if (authorResponse.IsOk())
            {
                bookDetails.Author = Mapper.Map(authorResponse.Result);
            }

            return new ServiceResult<BookDetails>(bookDetails);
        }


        public async Task<ServiceResult<Guid>> InsertBook(InsertBookDto insertBookDto)
        {
            // Create new book
            BookPOCO newBook = Mapper.Map(insertBookDto);
            newBook.Id = Guid.NewGuid();

            var newBookResponse = await bookRepository.InsertBook(newBook);
            if (!newBookResponse.IsOk()) return new ServiceResult<Guid>(new Guid(), newBookResponse.Code, newBookResponse.Message);

            // Assign new status to book
            StatusHistoryPOCO newStatusHistory = new StatusHistoryPOCO
            {
                BookId = newBookResponse.Result.Id,
                Status = "InStock",
                ModifiedDate = newBook.PublicationDate ?? DateTime.Now,
            };
            var newBookStatusResponse = await bookRepository.InsertBookStatus(newStatusHistory);
            if (!newBookStatusResponse.IsOk()) return new ServiceResult<Guid>(new Guid(), newBookStatusResponse.Code, newBookStatusResponse.Message);
            newBook.CurrentStatusId = newBookStatusResponse.Result.Id;

            // Assign status guid to book
            var updateResponse = await bookRepository.UpdateBook(newBook);
            if (!updateResponse.IsOk()) return new ServiceResult<Guid>(new Guid(), updateResponse.Code, updateResponse.Message);

            // If author is not null
            if (insertBookDto.AuthorId.HasValue)
            {
                BookAuthorPOCO bookAuthorConnection = new BookAuthorPOCO
                {
                    AuthorId = insertBookDto.AuthorId.Value,
                    BookId = newBook.Id
                };
                var addAuthorResponse = await authorRepository.AssignBookToAuthor(bookAuthorConnection);
                if (!addAuthorResponse.IsOk()) return new ServiceResult<Guid>(new Guid(), addAuthorResponse.Code, addAuthorResponse.Message);
            }

            return new ServiceResult<Guid>(result: newBook.Id);
        }
    }
}
