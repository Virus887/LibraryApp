using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.POCOs;
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

        public async Task<BookAuthorPOCO> AssignBookToAuthor(BookAuthorPOCO bookAuthorPOCO)
        {
            if (bookAuthorPOCO == null)
                throw new Exception("bookAuthor is null");

            try
            {
                await dbContext.BookAuthors.AddAsync(bookAuthorPOCO);
                await dbContext.SaveChangesAsync();

                return bookAuthorPOCO;
            }
            catch (Exception e)
            {
                return null;
            }
        }

        public IQueryable<BookAuthorPOCO> GetAllBookAuthors()
        {
            return dbContext.BookAuthors;
        }

        public AuthorPOCO GetBookAuthorsById(Guid bookId)
        {
            var bookAuthor = dbContext.BookAuthors.Where(x => x.BookId == bookId).FirstOrDefault();
            if(bookAuthor != null)
            {
                return bookAuthor.Author ?? null;
            }
            else
            {
                return null;
            }
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
