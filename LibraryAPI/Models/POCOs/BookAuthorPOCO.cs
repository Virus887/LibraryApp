using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models.POCO
{
    public partial class BookAuthorPOCO
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }

        public AuthorPOCO Author { get; set; }
        public BookPOCO Book { get; set; }
    }
}
