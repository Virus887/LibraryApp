using LibraryAPI.Enums;
using LibraryAPI.Models.DTOs;
using LibraryAPI.Models.POCOs;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.Database.Mapper
{
    public class Mapper
    {
        #region Book
        public static Book Map(BookPOCO book)
        {
            if (book == null) return null;

            return new Book
            {
                Id = book.Id,
                Title = book.Title
            };
        }
        public static BookStatus Map(StatusHistoryPOCO statusHistory)
        {
            if (statusHistory == null) return null;

            return new BookStatus
            {
                Id = statusHistory.Id,
                BookId = statusHistory.BookId,
                Status = Enum.TryParse<Statuses>(statusHistory.Status, out Statuses status) ? status : 0,
                ModifiedDate = statusHistory.ModifiedDate
            };
        }

        public static IEnumerable<BookStatus> Map(IEnumerable<StatusHistoryPOCO> statusHistories)
        {
            if (statusHistories == null) return null;

            var result = new List<BookStatus>();
            foreach(var status in statusHistories)
            {
                result.Add(Map(status));
            }
            return result;
        }

        public static BookPOCO Map(InsertBookDto bookDto)
        {
            if (bookDto == null) return null;

            return new BookPOCO
            {
                PublicationDate = bookDto.PublicationDate ?? DateTime.MinValue,
                Title = bookDto.Title,
                Language = bookDto.Language,
                Genre = bookDto.Genre?.ToString() ?? null
            };
        }

        public static BookDetails MapBookDetails(BookPOCO book)
        {
            if (book == null) return null;

            bool x = (book.Language == "Polski" || book.Language == "polski");

            return new BookDetails
            {
                Id = book.Id,
                Title = book.Title,
                PublicationDate = book.PublicationDate ?? DateTime.MinValue,
                Genre = Enum.TryParse<BookGenres>(book.Genre, out BookGenres genre) ? genre : 0,
                IsPolish = (book.Language == "Polski" || book.Language == "polski"),
                Language = book.Language,
            };
        }

        #endregion

        #region Autor
        public static Author Map(AuthorPOCO author)
        {
            if (author == null) return null;

            return new Author
            {
                Name = author.FirstName + " " + author.LastName,
                DateOfBirth = author.DateOfBirth ?? DateTime.MinValue
            };
        }

        #endregion

    }
}
