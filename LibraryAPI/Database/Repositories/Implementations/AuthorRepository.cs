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

                return new ServiceResult<BookAuthorPOCO>(bookAuthorPOCO);
            }
            catch (Exception e)
            {
                return ServiceResult<BookAuthorPOCO>.GetInternalErrorResult();
            }
        }

        public ServiceResult<IQueryable<BookAuthorPOCO>> GetAllBookAuthors()
        {
            var result = dbContext.BookAuthors;
            var books = dbContext.Books.ToList();
            var authors = dbContext.Authors.ToList();
            foreach(var bookauthor in result)
            {
                bookauthor.Book = books.Where(x => x.Id == bookauthor.BookId).FirstOrDefault();
                bookauthor.Author = authors.Where(x => x.Id == bookauthor.AuthorId).FirstOrDefault();
            }
            return new ServiceResult<IQueryable<BookAuthorPOCO>>(result);
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
                return new ServiceResult<AuthorPOCO>(bookAuthor.Author);
            }
        }

        public ServiceResult<AuthorPOCO> GetById(Guid id)
        {
            var author = dbContext.Find<AuthorPOCO>(id);

            if (author == null)
            {
                return ServiceResult<AuthorPOCO>.GetResourceNotFoundResult();
            }

            return new ServiceResult<AuthorPOCO>(author);
        }


    }
}
