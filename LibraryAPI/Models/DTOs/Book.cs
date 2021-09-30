using System;

namespace LibraryAPI.Models.DTO
{
    public class Book
    {
        public Guid Id { get; set; }
        public string Title { get; set; }
        public Author Author { get; set; }
    }
}
