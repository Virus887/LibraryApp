using LibraryAPI.Enums;
using System;

namespace LibraryAPI.Models.DTOs
{
    public class BookStatus
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public Statuses Status { get; set; }
    }
}
