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
            using (var client = new HttpClient())
            {
                var requestUri = new Uri($"http://www.omdbapi.com/?apikey=4f731024&s={title}");
                var response = await client.GetAsync(requestUri);

                var txtResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var searchResponse = JsonConvert.DeserializeObject<SearchResponse>(txtResult);

                if (response.IsSuccessStatusCode)
                    return searchResponse;

                return new SearchResponse();
            }
        }

        public async Task<MovieDetailResponse> GetDetail(string imdbId)
        {
            using (var client = new HttpClient())
            {
                var requestUri = new Uri($"http://www.omdbapi.com/?apikey=4f731024&i={imdbId}");
                var response = await client.GetAsync(requestUri);

                var txtResult = await response.Content.ReadAsStringAsync().ConfigureAwait(false);
                var movieDetailResponse = JsonConvert.DeserializeObject<MovieDetailResponse>(txtResult);

                if (response.IsSuccessStatusCode)
                    return movieDetailResponse;

                return new MovieDetailResponse();
            }
        }


    }
}
