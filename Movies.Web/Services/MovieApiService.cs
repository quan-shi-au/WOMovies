using Movies.Web.Models;
using Movies.Web.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Web.Services
{
    public class MovieApiService : IMovieApiService
    {
        public async Task<SearchResponse> SearchByTitle(string title)
        {
            HttpClient client = new HttpClient();

            var requestUri = new Uri($"http://www.omdbapi.com/?apikey=4f731024&s=Avengers");
            var response = await client.GetAsync(requestUri);

            var txtResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
            var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(txtResult);

            if (response.IsSuccessStatusCode)
                return searchResponse;

            return null;

        }
    }
}
