using Movies.Web.Models;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Movies.Web.Services.Interfaces
{
    public interface IJsonLdService
    {
        JObject GetMoviesJObject(List<MovieResponse> movies);
        JObject GetMovieDetailObject(MovieDetailResponse movie);
    }
}
