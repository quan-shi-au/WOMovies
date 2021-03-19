using Microsoft.AspNetCore.Mvc;
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

        public async Task<IActionResult> Index()
        {
            var searchResponse = await _movieApiService.SearchByTitle("");

            return View(searchResponse);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
