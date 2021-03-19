using Microsoft.AspNetCore.Mvc;
using Movies.Web.Infrastructure;
using Movies.Web.Models;
using Movies.Web.Models.Movies;
using Movies.Web.Services.Interfaces;
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

            return View("Index", searchResponse);
        }

    }
}
