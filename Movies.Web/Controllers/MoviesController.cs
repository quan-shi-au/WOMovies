using Microsoft.AspNetCore.Mvc;
using Movies.Web.Models.Movies;

namespace Movies.Web.Controllers
{
    public class MoviesController : Controller
    {
        public IActionResult Index()
        {
            var model = new SearchResults();
            model.LoadSampleData();
            return View(model);
        }

        public IActionResult Details()
        {
            return View();
        }
    }
}
