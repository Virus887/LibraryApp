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
        private IBookRepository bookRepository;
        private IAuthorRepository authorRepository;
        
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public bool ChangeBookStatus(Guid bookId, Statuses status)
        {
            StatusHistoryPOCO newHistoryStatus = new StatusHistoryPOCO
            {
                BookId = bookId,
                Status = status.ToString(),
                ModifiedDate = DateTime.Now
            };
            var newStatusId = bookRepository.InsertBookStatus(newHistoryStatus).Result.Id;

            // Change book current status
            var bookToChange =  bookRepository.GetById(bookId);
            bookToChange.CurrentStatusId = newStatusId;
            return true;
        }

        public IEnumerable<Book> GetAllBooks()
        {
            var books = bookRepository.GetAll();
            var bookDTOs = new List<Book>();
            foreach (var book in books)
            {
                bookDTOs.Add(
                    new Book
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = null
                    });
            }
            return bookDTOs.AsQueryable();
        }

        public IEnumerable<Book> GetBooksForPage(int page, int limit)
        {
            var books = bookRepository.GetAll().OrderBy(x=>x.Title).Skip(page*limit).Take(limit).AsQueryable();
            var authors = authorRepository.GetAllBookAuthors().ToList();
            var bookDTOs = new List<Book>();

            foreach(var book in books)
            {
                //var authorPoco = authorRepository.GetBookAuthorsById(book.Id);
                var authorPoco = authors.Where(x => x.BookId == book.Id).FirstOrDefault()?.Author;
                bookDTOs.Add(new Book
                {
                    Id = book.Id,
                    Title = book.Title,
                    Author = (authorPoco == null) ? null : new Author
                    {
                        Name = authorPoco.FirstName + " " + authorPoco.LastName,
                        DateOfBirth = authorPoco.DateOfBirth ?? DateTime.MinValue
                    }
                });
            }
            

            return bookDTOs;
        }


        //DONE
        public IEnumerable<BookStatus> GetBookStatuses(Guid bookId)
        {
            var statusHistory = bookRepository.GetStatusHistoryByBookId(bookId).OrderBy(x => x.ModifiedDate);
            var statuses = new List<BookStatus>();
            foreach(var status in statusHistory)
            {
                statuses.Add(
                    new BookStatus
                    {
                        Id=status.Id,
                        BookId = status.BookId,
                        ModifiedDate = status.ModifiedDate,
                        Status = Enum.Parse<Enums.Statuses>(status.Status)
                    });
            }

            return statuses.ToList();
        }


        //Może najpierw dodać książkę, później status, a później w książce ustawić status guid?
        public async Task<Guid> InsertBook(InsertBookDto insertBookDto)
        {
            // Create new book
            BookPOCO newBook = new BookPOCO
            {
                Id = Guid.NewGuid(),
                Title = insertBookDto.Title,
                Language = insertBookDto.Language,
                PublicationDate = insertBookDto.PublicationDate ?? null,
                PageNumber = null,
                Genre = insertBookDto.Genre.ToString() ?? null
            };

            Guid newBookId = bookRepository.InsertBook(newBook).Result.Id;

            // Assign new status to book
            StatusHistoryPOCO newStatusHistory = new StatusHistoryPOCO
            {
                BookId = newBookId,
                Status = "InStock",
                //Warning: if publication date is null, we assign DateTime.MinValue
                ModifiedDate = newBook.PublicationDate ?? DateTime.MinValue,
            };

            var newBookStatusId = bookRepository.InsertBookStatus(newStatusHistory).Result.Id;
            newBook.CurrentStatusId = newBookStatusId;
            await bookRepository.UpdateBook(newBook);

            // If author is not null
            if(insertBookDto.AuthorId.HasValue)
            {
                BookAuthorPOCO bookAuthorConnection = new BookAuthorPOCO
                {
                    AuthorId = insertBookDto.AuthorId.Value,
                    BookId = newBookId
                };
                await authorRepository.AssignBookToAuthor(bookAuthorConnection);
            }

            return newBookId;
        }
    }
}
