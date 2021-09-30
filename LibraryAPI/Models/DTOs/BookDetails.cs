using LibraryAPI.Enums;
using System;

namespace LibraryAPI.Models.DTO
{
    public class BookDetails
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime PublicationDate { get; set; }
        public BookGenres Genre { get; set; }
        public Statuses CurrentStatus { get; set; }
        public Author Author { get; set; }

        /// <summary>
        /// Should be true if Language is equal to "Polski" or "polski", otherwise false
        /// </summary>
        public bool IsPolish { get; set; }

        /// <summary>
        /// Should be taken from given API
        /// </summary>
        public double CurrentPrice { get; set; }
    }
}
