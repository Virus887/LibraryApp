using LibraryAPI.Models.DTO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services.Interfaces
{
    public interface IBookService
    {
        public IQueryable<Book> GetAll();
    }
}
