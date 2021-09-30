using LibraryAPI.Enums;
using System;

namespace LibraryAPI.Models.DTO
{
    public class InsertBookDto
    {
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime? PublicationDate { get; set; }
        public BookGenres? Genre { get; set; }
        public Guid? AuthorId { get; set; }
    }
}
