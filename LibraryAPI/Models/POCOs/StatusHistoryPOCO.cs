using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;

#nullable disable

namespace LibraryAPI.Models.POCOs
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
