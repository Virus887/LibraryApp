using LibraryAPI.Models.POCO;
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
    }
}
