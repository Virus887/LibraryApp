using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.POCOs;
using LibraryAPI.Services;
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

        public async Task<ServiceResult<BookAuthorPOCO>> AssignBookToAuthor(BookAuthorPOCO bookAuthorPOCO)
        {
            if (bookAuthorPOCO == null)
            {
                return ServiceResult<BookAuthorPOCO>.GetEntityNullResult();
            }      
            try
            {
                await dbContext.BookAuthors.AddAsync(bookAuthorPOCO);
                await dbContext.SaveChangesAsync();

                return new ServiceResult<BookAuthorPOCO>(result: bookAuthorPOCO);
            }
            catch (Exception e)
            {
                return ServiceResult<BookAuthorPOCO>.GetInternalErrorResult();
            }
        }

        public ServiceResult<IQueryable<BookAuthorPOCO>> GetAllBookAuthors()
        {
            return new ServiceResult<IQueryable<BookAuthorPOCO>>(result: dbContext.BookAuthors);
        }

        public ServiceResult<AuthorPOCO> GetAuthorOfBook(Guid bookId)
        {
            BookAuthorPOCO bookAuthor = dbContext.BookAuthors.Where(x => x.BookId == bookId).FirstOrDefault();
            if(bookAuthor == null)
            {
                return ServiceResult<AuthorPOCO>.GetResourceNotFoundResult();
            }
            else
            {
                return new ServiceResult<AuthorPOCO>(result: bookAuthor.Author);
            }
        }

        public ServiceResult<AuthorPOCO> GetById(Guid id)
        {
            var author = dbContext.Find<AuthorPOCO>(id);

            if (author == null)
            {
                return ServiceResult<AuthorPOCO>.GetResourceNotFoundResult();
            }

            return new ServiceResult<AuthorPOCO>(result: author);
        }


    }
}
