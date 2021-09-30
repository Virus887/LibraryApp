using LibraryAPI.Models.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public AuthorPOCO GetById(Guid authorId);
        public AuthorPOCO GetAuthorOfBook(Guid bookId);
        public IQueryable<BookAuthorPOCO> GetAllBookAuthors(); 
        public Task<BookAuthorPOCO> AssignBookToAuthor(BookAuthorPOCO bookAuthorPOCO);
    }
}
