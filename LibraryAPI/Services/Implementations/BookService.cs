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
        
        public BookService(IBookRepository bookRepository)
        {
            this.bookRepository = bookRepository;
        }

        public IQueryable<Book> GetAll()
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
            return bookDTOs.AsQueryable<Book>();
        }
    }
}
