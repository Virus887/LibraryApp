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

        public BookPOCO GetById(Guid bookId)
        {
            var result = dbContext.Books.Find(bookId);

            if (result == null)
            {
                throw new Exception("There is no book with given Id.");
            }

            return result;
        }

        public IQueryable<StatusHistoryPOCO> GetStatusHistoryByBookId(Guid bookId)
        {
            var result = dbContext.StatusHistories.Where(x => x.BookId == bookId);

            if (result == null)
            {
                throw new Exception("There is no book with given Id.");
            }

            return result;
        }

    }
}
