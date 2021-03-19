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

    }
}
