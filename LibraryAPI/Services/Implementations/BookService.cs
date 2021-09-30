﻿using LibraryAPI.Database.Repositories.Interfaces;
using LibraryAPI.Models.DTOs;
using LibraryAPI.Models.POCOs;
using LibraryAPI.Services.Interfaces;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Services.Implementations
{
    public class BookService : IBookService
    {
        private IBookRepository bookRepository;
        private IAuthorRepository authorRepository;
        
        public BookService(IBookRepository bookRepository, IAuthorRepository authorRepository)
        {
            this.bookRepository = bookRepository;
            this.authorRepository = authorRepository;
        }

        public IEnumerable<Book> GetAll()
        {
            var books = bookRepository.GetAll();
            var bookDTOs = new List<Book>();
            foreach (var book in books)
            {
                bookDTOs.Add(
                    new Book
                    {
                        Id = book.Id,
                        Title = book.Title,
                        Author = null
                    });
            }
            return bookDTOs.AsQueryable();
        }


        //DONE
        public IEnumerable<BookStatus> GetBookStatuses(Guid bookId)
        {
            var statusHistory = bookRepository.GetStatusHistoryByBookId(bookId).OrderBy(x => x.ModifiedDate);
            var statuses = new List<BookStatus>();
            foreach(var status in statusHistory)
            {
                statuses.Add(
                    new BookStatus
                    {
                        Id=status.Id,
                        BookId = status.BookId,
                        ModifiedDate = status.ModifiedDate,
                        Status = Enum.Parse<Enums.Statuses>(status.Status)
                    });
            }

            return statuses.ToList();
        }


        //Możr najpierw dodać książkę, później status, a później w książce ustawić status guid?
        public async Task<Guid> InsertBook(InsertBookDto insertBookDto)
        {
            //create new book

            BookPOCO newBook = new BookPOCO
            {
                Id = Guid.NewGuid(),
                Title = insertBookDto.Title,
                Language = insertBookDto.Language,
                PublicationDate = insertBookDto.PublicationDate ?? null,
                PageNumber = null,
                CurrentStatusId = Guid.NewGuid(),
                Genre = insertBookDto.Genre.ToString() ?? null
            };

            Guid newBookId = bookRepository.InsertBook(newBook).Result.Id;

            //Assign new status to book
            StatusHistoryPOCO newStatusHistory = new StatusHistoryPOCO
            {
                Id = newBook.CurrentStatusId ?? new Guid(),
                BookId = newBookId,
                Status = "InStock",
                //Warning: if publication date is null, we assign DateTime.MinValue
                ModifiedDate = newBook.PublicationDate ?? DateTime.MinValue,
            };

            var newBookStatusId = bookRepository.InsertBookStatus(newStatusHistory).Result.Id;
            newBook.CurrentStatusId = newBookStatusId;
            await bookRepository.UpdateBook(newBook);

            //If author is not null
            if(insertBookDto.AuthorId.HasValue)
            {
                BookAuthorPOCO bookAuthorConnection = new BookAuthorPOCO
                {
                    AuthorId = insertBookDto.AuthorId.Value,
                    BookId = newBookId
                };
                await authorRepository.AssignBookToAuthor(bookAuthorConnection);
            }

            return newBookId;
        }
    }
}
