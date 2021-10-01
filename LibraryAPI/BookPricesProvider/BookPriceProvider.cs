using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace LibraryAPI.BookPricesProvider
{
    public class BookPriceProvider : IBookPriceProvider
    {
        private readonly IHttpClientFactory clientFactory;
        public BookPriceProvider (IHttpClientFactory clientFactory)
        {
            this.clientFactory = clientFactory;
        }
        public async Task<double?> GetBookPrice(Guid bookId)
        {
            var client = clientFactory.CreateClient("bookPriceAPI");
            var result = await client.GetAsync($"Prices/{bookId.ToString().ToLower()}");
            var jsonString = await result.Content.ReadAsStringAsync();


            if (result.IsSuccessStatusCode)
            {
                var response = JsonConvert.DeserializeObject<BookPriceResponse>(jsonString);
                return response.Price;
            }
            else
            {
                return null;
            }
        }
    }
}
