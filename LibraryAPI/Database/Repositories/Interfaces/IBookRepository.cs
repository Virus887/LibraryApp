using LibraryAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Interfaces
{
    public interface IBookRepository
    {
        public BookPOCO GetById(Guid id);
        public IQueryable<BookPOCO> GetAll();

    }
}
