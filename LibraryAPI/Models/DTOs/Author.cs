using System;

namespace LibraryAPI.Models.DTO
{
    public class Author
    {
        /// <summary>
        /// First name and last name separated with space
        /// </summary>
        public string Name { get; set; }
        public DateTime DateOfBirth { get; set; }
    }
}
