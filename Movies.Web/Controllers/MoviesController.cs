using JsonLD.Core;
using Microsoft.AspNetCore.Mvc;
using Movies.Web.Infrastructure;
using Movies.Web.Models;
using Movies.Web.Models.Movies;
using Movies.Web.Services.Interfaces;
using Newtonsoft.Json;
using Newtonsoft.Json.Linq;
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

            var json = "{'@context':{'test':'http://www.test.com/'},'test:hello':'world'}";
            var document = JObject.Parse(json);
            var expanded = JsonLdProcessor.Expand(document);

            ViewBag.LdJsonObject = expanded;


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

            var json = JsonConvert.SerializeObject(searchResponse.Search);
            var document = JObject.Parse(json);
            var expanded = JsonLdProcessor.Expand(document);


            return View("Index", searchResponse);
        }

    }
}
