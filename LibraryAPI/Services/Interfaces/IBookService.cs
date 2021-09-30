using LibraryAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        //TODO: change to get all books
        public IEnumerable<Book> GetAll();
        public IEnumerable<BookStatus> GetBookStatuses(Guid bookId);

        public int InsertBook(InsertBookDto insertBookDto);
    }
}
