using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models.POCO
{
    public partial class StatusHistory
    {
        public Guid Id { get; set; }
        public Guid BookId { get; set; }
        public DateTime ModifiedDate { get; set; }
        public string Status { get; set; }

        public virtual Book Book { get; set; }
    }
}
