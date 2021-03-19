using JsonLD.Core;
using Microsoft.AspNetCore.Mvc;
using Movies.Web.Infrastructure;
using Movies.Web.Models;
using Movies.Web.Models.Movies;
using Movies.Web.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;
using System.Threading.Tasks;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        private readonly IMovieApiService _movieApiService;

        public MoviesController(IMovieApiService movieApiService)
        {
            _movieApiService = movieApiService;
        }

        public IActionResult Index()
        {
            return View(new SearchResponse());
        }

        public IActionResult Details()
        {
            return View();
        }

        [HttpPost]
        public async Task<IActionResult> Index(string title)
        {
            var searchResponse = await _movieApiService.SearchByTitle(title);

            SetJObject(searchResponse.Search);

            return View("Index", searchResponse);
        }

        private void SetJObject(List<MovieResponse> movies)
        {

            var movieList = @"
{
    '@id': 'http://example.org/movies',
    'http://schema.org/name': 'Movies',
    'http://schema.org/items': [";

            var isFirst = true;
            foreach (var movie in movies)
            {
                if (isFirst)
                    isFirst = false;
                else
                    movieList += ",";

                movieList += @"
    {
        '@type': 'http://schema.org/Movie',
        'http://schema.org/name': '" + movie.Title.Replace("'", "") + @"',
        'http://schema.org/year': {'@id': '" + movie.Year + @"'},
        'http://schema.org/image': {'@id': '" + movie.Poster + @"'}
    }
";
            }

movieList += "]}";

            var _contextJson = @"
{
    'name': 'http://schema.org/name',
    'list': 'http://schema.org/items',
    'year': {'@id': 'http://schema.org/year', '@type': '@id'},
    'image': {'@id': 'http://schema.org/image', '@type': '@id'},
    'Movie': 'http://schema.org/Movie'
}
";

            var doc = JObject.Parse(movieList);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);

            ViewBag.LdJsonObject = compacted;
        }

    }
}
