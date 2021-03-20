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

        public async Task<IActionResult> Index(string title)
        {
            var searchResponse = new SearchResponse();
            ViewBag.JsonLdObject = _jsonLdService.GetMoviesJObject(searchResponse.Search);

            if (!string.IsNullOrEmpty(title))
            {
                searchResponse = await _movieApiService.SearchByTitle(title);
                searchResponse.Title = title;
                ViewBag.JsonLdObject = _jsonLdService.GetMoviesJObject(searchResponse.Search);
            }

            return View(searchResponse);
        }

        public async Task<IActionResult> Details(string imdbId)
        {
            var movieDetailResponse = await _movieApiService.GetDetail(imdbId);
            ViewBag.JsonLdObject = _jsonLdService.GetMovieDetailObject(movieDetailResponse);

            return View(movieDetailResponse);
        }

        [HttpPost]
        public IActionResult Search(string title)
        {
            return RedirectToAction("Index", new { title });
        }


    }
}
