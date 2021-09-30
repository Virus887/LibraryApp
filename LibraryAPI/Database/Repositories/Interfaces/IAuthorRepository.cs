using LibraryAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public AuthorPOCO GetById(Guid id); 
    }
}
