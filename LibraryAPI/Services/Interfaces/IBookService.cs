using LibraryAPI.Enums;
using LibraryAPI.Models.DTOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        public IEnumerable<Book> GetAllBooks();
        public IEnumerable<Book> GetBooksForPage(int page, int limit);
        public IEnumerable<BookStatus> GetBookStatuses(Guid bookId);
        public Task<Guid> InsertBook(InsertBookDto insertBookDto);
        public bool ChangeBookStatus(Guid bookId, Statuses status);

    }
}
