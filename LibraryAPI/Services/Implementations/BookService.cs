using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.DTO;
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

        public IEnumerable<Book> GetAll()
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
                        Status = status.Status switch
                        {
                            "InStock" => Enums.Statuses.InStock,
                            "Borrowed" => Enums.Statuses.Borrowed,
                            "Sold" => Enums.Statuses.Sold,
                            "Missing" => Enums.Statuses.Missing,
                            _ => Enums.Statuses.Missing
                        }
                    });
            }

            return statuses.ToList();
        }

        public int InsertBook(InsertBookDto insertBookDto)
        {
            throw new NotImplementedException();
        }


    }
}
