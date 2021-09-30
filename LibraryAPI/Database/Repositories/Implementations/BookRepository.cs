using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.POCOs;
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

        public async Task<BookPOCO> InsertBook(BookPOCO bookPOCO)
        {
            if (bookPOCO == null)
                throw new Exception("book is null");

            try
            {
                await dbContext.Books.AddAsync(bookPOCO);
                await dbContext.SaveChangesAsync();

                return bookPOCO;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<BookPOCO> UpdateBook(BookPOCO bookPOCO)
        {
            if (bookPOCO == null)
            {
                throw new Exception("book is null");
            }
            try
            {
                dbContext.Books.Update(bookPOCO);
                await dbContext.SaveChangesAsync();
                return bookPOCO;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public async Task<StatusHistoryPOCO> InsertBookStatus(StatusHistoryPOCO statusHistoryPOCO)
        {
            if (statusHistoryPOCO == null)
                throw new Exception("book is null");

            try
            {
                await dbContext.StatusHistories.AddAsync(statusHistoryPOCO);
                await dbContext.SaveChangesAsync();

                return statusHistoryPOCO;
            }
            catch (Exception e)
            {
                return null;
            }
        }
    }
}
