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
        private readonly IJsonLdService _jsonLdService;

        public MoviesController(
            IMovieApiService movieApiService,
            IJsonLdService jsonLdService
            )
        {
            _movieApiService = movieApiService;
            _jsonLdService = jsonLdService;
        }

        public IActionResult Index()
        {
            ViewBag.JsonLdObject = _jsonLdService.GetMoviesJObject(new SearchResponse().Search);
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
            searchResponse.Title = title;
            ViewBag.JsonLdObject = _jsonLdService.GetMoviesJObject(searchResponse.Search);

            return View("Index", searchResponse);
        }


    }
}
