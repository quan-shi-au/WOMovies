using Microsoft.AspNetCore.Mvc;
using Movies.Web.Models;
using Movies.Web.Services.Interfaces;
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
            if (!string.IsNullOrEmpty(title))
            {
                var searchResponse = await _movieApiService.SearchByTitle(title);
                searchResponse.Title = title;

                if (searchResponse.Search.Count > 0)
                    ViewBag.JsonLdObject = _jsonLdService.GetMoviesJObject(searchResponse.Search);
                
                return View(searchResponse);
            }

            return View(new SearchResponse());
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
