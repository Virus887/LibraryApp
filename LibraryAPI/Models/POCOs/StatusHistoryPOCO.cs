using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models.POCO
{
    public partial class StatusHistoryPOCO
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }

        public BookPOCO Book { get; set; }
    }
}
