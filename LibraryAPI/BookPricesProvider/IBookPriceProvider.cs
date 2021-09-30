using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace LibraryAPI.BookPricesProvider
{
    public interface IBookPriceProvider
    {
        public Task<double> GetBookPrice(Guid bookId);
    }
}
