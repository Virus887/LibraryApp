using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.BookPricesProvider
{
    public class BookPriceResponse
    {
        public Guid Id { get; set; }
        public double Price { get; set; }
    }
}
