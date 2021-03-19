using Microsoft.AspNetCore.Mvc;
using Movies.Web.Models.Movies;
using Movies.Web.Services.Interfaces;

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
            var x = _movieApiService.SearchByTitle("");

            var model = new SearchResults(null);
            model.LoadSampleData();
            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
