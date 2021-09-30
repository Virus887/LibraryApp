using System;
using System.Collections.Generic;

#nullable disable

namespace LibraryAPI.Models.POCO
{
    public partial class AuthorPOCO
    {
        public Guid Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public DateTime? DateOfBirth { get; set; }
    }
}
