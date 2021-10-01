using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Enums;
using LibraryAPI.Models.POCOs;
using LibraryAPI.Services;
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

        public ServiceResult<IQueryable<BookPOCO>> GetAll()
        {
            return new ServiceResult<IQueryable<BookPOCO>>(dbContext.Books);
        }

        public ServiceResult<BookPOCO> GetById(Guid bookId)
        {
            var result = dbContext.Books.Find(bookId);

            if (result == null)
            {
                return ServiceResult<BookPOCO>.GetResourceNotFoundResult();
            }

            return new ServiceResult<BookPOCO>(result: result);
        }


        public ServiceResult<IQueryable<StatusHistoryPOCO>> GetStatusHistoryByBookId(Guid bookId)
        {
            var result = dbContext.StatusHistories.Where(x => x.BookId == bookId);

            if (result == null)
            {
                return ServiceResult<IQueryable<StatusHistoryPOCO>>.GetResourceNotFoundResult();
            }

           return new ServiceResult<IQueryable<StatusHistoryPOCO>>(result: result);
        }

        public async Task<ServiceResult<BookPOCO>> InsertBook(BookPOCO bookPOCO)
        {
            if (bookPOCO == null)
            {
                return ServiceResult<BookPOCO>.GetEntityNullResult();
            }

            try
            {
                await dbContext.Books.AddAsync(bookPOCO);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<BookPOCO>(result: bookPOCO);
            }
            catch (Exception e)
            {
                return ServiceResult<BookPOCO>.GetInternalErrorResult();
            }
        }

        public async Task<ServiceResult<BookPOCO>> UpdateBook(BookPOCO bookPOCO)
        {
            if (bookPOCO == null)
            {
                return ServiceResult<BookPOCO>.GetEntityNullResult();
            }
            try
            {
                dbContext.Books.Update(bookPOCO);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<BookPOCO>(result: bookPOCO);
            }
            catch (Exception e)
            {
                return ServiceResult<BookPOCO>.GetInternalErrorResult();
            }
        }

        public async Task<ServiceResult<StatusHistoryPOCO>> InsertBookStatus(StatusHistoryPOCO statusHistoryPOCO)
        {
            if (statusHistoryPOCO == null)
            {
                return ServiceResult<StatusHistoryPOCO>.GetEntityNullResult();
            }

            try
            {
                await dbContext.StatusHistories.AddAsync(statusHistoryPOCO);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<StatusHistoryPOCO>(result: statusHistoryPOCO);
            }
            catch (Exception e)
            {
                return ServiceResult<StatusHistoryPOCO>.GetInternalErrorResult();
            }
        }

        public ServiceResult<Statuses> GetBookCurrentStatus(Guid bookId)
        {
            var result = dbContext.StatusHistories.Where(x => x.BookId == bookId).FirstOrDefault();
           
            if (result == null)
            {
                return ServiceResult<Statuses>.GetResourceNotFoundResult();
            }

            if(Enum.TryParse<Statuses>(result.Status, out Statuses status))
            {
                return new ServiceResult<Statuses>(result: status);
            }
            else
            {
                return ServiceResult<Statuses>.GetResourceNotFoundResult();
            }

            
        }
    }
}
