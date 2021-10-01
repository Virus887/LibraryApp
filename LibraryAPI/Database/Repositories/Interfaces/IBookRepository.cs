using LibraryAPI.Enums;
using LibraryAPI.Models.POCOs;
using LibraryAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public ServiceResult<BookPOCO> GetById(Guid bookId);
        public ServiceResult<IQueryable<BookPOCO>> GetAll();

        public ServiceResult<IQueryable<StatusHistoryPOCO>> GetStatusHistoryByBookId(Guid bookId);

        public ServiceResult<Statuses> GetBookCurrentStatus(Guid bookId);

        public Task<ServiceResult<BookPOCO>> InsertBook(BookPOCO bookPOCO);

        public Task<ServiceResult<BookPOCO>> UpdateBook(BookPOCO bookPOCO);

        public Task<ServiceResult<StatusHistoryPOCO>> InsertBookStatus(StatusHistoryPOCO statusHistoryPOCO);

    }
}