using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Implementations
{
    public class AuthorRepository : IAuthorRepository
    {
        private LibraryDbContext dbContext;

        public AuthorRepository (LibraryDbContext context)
        {
            dbContext = context;
        }

        public AuthorPOCO GetById(Guid id)
        {
            var author = dbContext.Find<AuthorPOCO>(id);

            if (author == null)
            {
                throw new Exception("There is no user with given Id.");
            }

            return author;
        }
    }
}
