using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using LibraryAPI.Database;
using LibraryAPI.Enums;
using LibraryAPI.Models.DTOs;
using LibraryAPI.Services.Interfaces;
using Microsoft.AspNetCore.Mvc;

namespace LibraryAPI.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class LibraryController : ControllerBase
    {
        private readonly IBookService bookService;

        public LibraryController(IBookService bookService)
        {
            this.bookService = bookService;
        }
        /// <summary>
        /// Get list of books (Id and Title)
        /// </summary>
        /// <param name="page">Page number</param>
        /// <param name="limit">Number of books on each page</param>
        /// <returns>List of Books sorted by book title</returns>
        [HttpGet]
        public ActionResult<IEnumerable<Book>> GetBooks(int page = 0, int limit = 10)
        {
            var result = bookService.GetBooksForPage(page, limit);
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Get details of a book
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <returns>Details of a book</returns>
        [HttpGet("details/{bookId}")]
        public ActionResult<BookDetails> GetBookDetails([FromRoute] Guid bookId)
        {
            var result = bookService.GetBookDetails(bookId);
            return Ok();
        }

        /// <summary>
        /// Get history of book statuses sorted by date (oldest to newest)
        /// </summary>
        /// <param name="bookId">Book Id</param>
        /// <returns>List of BookStatuses</returns>
        [HttpGet("statuses/{bookId}")]
        public ActionResult<IEnumerable<BookStatus>> GetBookStatuses([FromRoute] Guid bookId)
        {
            var result = bookService.GetBookStatuses(bookId).ToList();
            if (result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }

        /// <summary>
        /// Insert new book to the library
        /// </summary>
        /// <param name="insertBookDto">Book to create details</param>
        /// <returns>Id of new book</returns>
        [HttpPost]
        public async Task<ActionResult<Guid>> InsertBook([FromBody] InsertBookDto insertBookDto)
        {
            var result = await bookService.InsertBook(insertBookDto);
            if(result != null)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }

        }

        /// <summary>
        /// Change status of a book
        /// </summary>
        /// <param name="bookId">Bookd Id</param>
        /// <param name="status">New book status</param>
        [HttpPost("status/{bookId}")]
        public ActionResult ChangeBookStatus([FromRoute] Guid bookId, [FromBody] Statuses status)
        {
            var result = bookService.ChangeBookStatus(bookId, status);
            if(result)
            {
                return Ok(result);
            }
            else
            {
                return NotFound();
            }
        }
    }
}
