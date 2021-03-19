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

            var _docJson = @"
{
    '@id': 'http://example.org/ld-experts',
    'http://schema.org/name': 'LD Experts',
    'http://schema.org/member': [{
        '@type': 'http://schema.org/Person',
        'http://schema.org/name': 'Manu Sporny',
        'http://schema.org/url': {'@id': 'http://manu.sporny.org/'},
        'http://schema.org/image': {'@id': 'http://manu.sporny.org/images/manu.png'}
    },{
        '@type': 'http://schema.org/Person',
        'http://schema.org/name': 'Manu Sam',
        'http://schema.org/url': {'@id': 'http://hi.sporny.org/'},
        'http://schema.org/image': {'@id': 'http://xx.sporny.org/images/manu.png'}
    }]
}";

            var _contextJson = @"
{
    'name': 'http://schema.org/name',
    'member': 'http://schema.org/member',
    'homepage': {'@id': 'http://schema.org/url', '@type': '@id'},
    'image': {'@id': 'http://schema.org/image', '@type': '@id'},
    'Person': 'http://schema.org/Person',
    '@vocab': 'http://example.org/',
    '@base': 'http://example.org/'
}
";

            var doc = JObject.Parse(_docJson);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);

            ViewBag.LdJsonObject = compacted;


            //var json = "{'@context':{'test':'http://www.test.com/'},'test:hello':'world'}";
            //var document = JObject.Parse(json);
            //var expanded = JsonLdProcessor.Expand(document);

            //ViewBag.LdJsonObject = expanded;


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
