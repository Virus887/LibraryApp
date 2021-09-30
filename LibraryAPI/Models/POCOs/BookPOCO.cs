using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models.POCO
{
    public partial class Book
    {
        public Guid Id { get; set; }
        public Guid? CurrentStatusId { get; set; }
        public string Title { get; set; }
        public string Language { get; set; }
        public DateTime? PublicationDate { get; set; }
        public string Genre { get; set; }
        public int? PageNumber { get; set; }
    }
}
