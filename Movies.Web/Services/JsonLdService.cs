using JsonLD.Core;
using Movies.Web.Models;
using Movies.Web.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Services
{
    public class JsonLdService : IJsonLdService
    {
        public JObject GetMoviesJObject(List<MovieResponse> movies)
        {

            var movieList = @"
            {
                '@id': 'http://example.org/movies',
                'http://schema.org/name': 'Movies',
                'http://schema.org/items': [
            ";

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

            var movieListJObject = JObject.Parse(movieList);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();

            return JsonLdProcessor.Compact(movieListJObject, context, opts);

        }

        public JObject GetMovieDetailObject(MovieDetailResponse movie)
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
    }]
}
";

            var _contextJson = @"
{
    'name': 'http://schema.org/name',
    'member': 'http://schema.org/member',
    'homepage': {'@id': 'http://schema.org/url', '@type': '@id'},
    'image': {'@id': 'http://schema.org/image', '@type': '@id'},
    'Person': 'http://schema.org/Person',
    '@vocab': 'http://example.org/',
    '@base': 'http://example.org/'
}";

            var doc = JObject.Parse(_docJson);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);


            return compacted;

        }



    }
}
