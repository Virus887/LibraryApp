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
        public ServiceResult<IEnumerable<Book>> GetBooksForPage(int page, int limit);
        public ServiceResult<BookDetails> GetBookDetails(Guid bookId);
        public ServiceResult<IEnumerable<BookStatus>> GetBookStatuses(Guid bookId);
        public Task<ServiceResult<Guid>> InsertBook(InsertBookDto insertBookDto);
        public ServiceResult<bool> ChangeBookStatus(Guid bookId, Statuses status);

    }
}
