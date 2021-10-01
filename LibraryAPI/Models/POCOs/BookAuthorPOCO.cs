using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LibraryAPI.Models.POCOs
{
    public partial class BookAuthorPOCO
    {
        public Guid BookId { get; set; }
        public Guid AuthorId { get; set; }

        public AuthorPOCO Author { get; set; }
        public BookPOCO Book { get; set; }
    }
}
