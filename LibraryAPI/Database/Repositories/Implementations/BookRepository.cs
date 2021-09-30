using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.POCO;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Implementations
{
    public class BookRepository : IBookRepository
    {
        private LibraryDbContext dbContext;

        public BookRepository(LibraryDbContext context)
        {
            dbContext = context;
        }

        public IQueryable<BookPOCO> GetAll()
        {
            return dbContext.Books;
        }

        public BookPOCO GetById(Guid id)
        {
            var result = dbContext.Find<BookPOCO>(id);

            if (result == null)
            {
                throw new Exception("There is no book with given Id.");
            }

            return result;
        }
    }
}
