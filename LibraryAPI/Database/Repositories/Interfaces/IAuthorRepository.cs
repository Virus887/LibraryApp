using LibraryAPI.Models.POCOs;
using LibraryAPI.Services;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Repositories.Interfaces
{
    public interface IAuthorRepository
    {
        public ServiceResult<AuthorPOCO> GetById(Guid authorId);
        public ServiceResult<AuthorPOCO> GetAuthorOfBook(Guid bookId);
        public ServiceResult<IQueryable<BookAuthorPOCO>> GetAllBookAuthors(); 
        public Task<ServiceResult<BookAuthorPOCO>> AssignBookToAuthor(BookAuthorPOCO bookAuthorPOCO);
    }
}
