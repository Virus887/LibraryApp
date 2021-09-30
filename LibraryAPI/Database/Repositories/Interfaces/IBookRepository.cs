using LibraryAPI.Models.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public BookPOCO GetById(Guid bookId);
        public IQueryable<BookPOCO> GetAll();

        public IQueryable<StatusHistoryPOCO> GetStatusHistoryByBookId(Guid bookId);

        public Task<BookPOCO> InsertBook(BookPOCO bookPOCO);

        public Task<BookPOCO> UpdateBook(BookPOCO bookPOCO);

        public Task<StatusHistoryPOCO> InsertBookStatus(StatusHistoryPOCO statusHistoryPOCO);

    }
}