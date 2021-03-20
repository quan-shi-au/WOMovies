using Microsoft.Extensions.Options;
using Movies.Web.Models;
using Movies.Web.Services.Interfaces;
using Newtonsoft.Json;
using System;
using System.Net.Http;
using System.Threading.Tasks;

namespace Movies.Web.Services
{
    public class MovieApiService : IMovieApiService
    {
        private readonly MovieApiOptions _options;
        public MovieApiService(IOptions<MovieApiOptions> options)
        {
            _options = options.Value;
        }

        public async Task<SearchResponse> SearchByTitle(string title)
        {
            using (var client = new HttpClient())
            {
                var requestUri = new Uri($"http://www.omdbapi.com/?apikey={_options.ApiKey}&s={title}");
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
                var requestUri = new Uri($"http://www.omdbapi.com/?apikey={_options.ApiKey}&i={imdbId}");
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
