using JsonLD.Core;
using Movies.Web.Infrastructure;
using Movies.Web.Models;
using Movies.Web.Services.Interfaces;
using Newtonsoft.Json.Linq;
using System.Collections.Generic;

namespace Movies.Web.Services
{
    public class JsonLdService : IJsonLdService
    {
        private const string _moviesContextJson = @"
        {
            'name': 'http://schema.org/name',
            'ListItem': 'http://schema.org/ListItem',
            'item': 'http://schema.org/item',
            'itemListElement': 'http://schema.org/itemListElement',
            'image': {'@id': 'http://schema.org/image', '@type': '@id'},
            'Movie': 'http://schema.org/Movie',
            'dateCreated': 'http://schema.org/dateCreated',
            'url': 'http://schema.org/url',
            'position': 'http://schema.org/position',
            'dateCreated': 'http://schema.org/dateCreated',
            'director': 'http://schema.org/director'
        }";

        private const string _movieContextJson = @"
        {
            'name': 'http://schema.org/name',
            'member': 'http://schema.org/items',
            'image': {'@id': 'http://schema.org/image', '@type': '@id'},
            'Movie': 'http://schema.org/Movie',
            'dateCreated': 'http://schema.org/dateCreated',
            'review': 'http://schema.org/review',
            'reviewRating': 'http://schema.org/reviewRating',
            'ratingValue': 'http://schema.org/ratingValue',
            'reviewBody': 'http://schema.org/reviewBody',
            'author': 'http://schema.org/author',
            'actor': 'http://schema.org/actor',
            'director': 'http://schema.org/director'
        }
        ";


        public JObject GetMoviesJObject(List<MovieResponse> movies)
        {
            var docJson = @"
            {
                '@id': 'http://example.org/movies',
                'http://schema.org/name': 'Movies',
                '@type': 'http://schema.org/ItemList',
                'http://schema.org/itemListElement': [";

            docJson = AddMovieArray(movies, docJson);

            docJson += "]}";

            var doc = JObject.Parse(docJson.UnEscapeString());
            var context = JObject.Parse(_moviesContextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);

            return compacted;

        }

        private string AddMovieArray(List<MovieResponse> movies, string docJson)
        {
            var position = 1;
            foreach (var movie in movies)
            {
                if (position > 1)
                    docJson += ",";

                docJson += @"
                {
                    '@type': 'http://schema.org/ListItem',
                    'http://schema.org/position': " + position + @",
                    'http://schema.org/item': {
                        '@type': 'http://schema.org/Movie',
                        'http://schema.org/name': '" + movie.Title.EscapeString() + @"',
                        'http://schema.org/image': { '@id': '" + movie.Poster + @"'},
                        'http://schema.org/dateCreated' : '" + movie.Year.ValidateYear() + @"',
                        'http://schema.org/director': 'n/a',
                        'http://schema.org/url': '" + movie.Poster + @"'
                        }
                }";
                position++;
            }

            return docJson;
        }

        public JObject GetMovieDetailObject(MovieDetailResponse movie)
        {
            var rating = GetRating(movie);

            var docJson = @"
            {
                '@id': 'http://example.org/movies',
                'http://schema.org/name': '" + movie.Title.EscapeString() + @"',
                '@type': 'http://schema.org/Movie',
                'http://schema.org/image': {'@id': '" + movie.Poster + @"'},
                'http://schema.org/dateCreated' : '" + movie.Year.ValidateYear() + @"',
                'http://schema.org/director': '" + movie.Director.EscapeString() + @"',
                'http://schema.org/review': {
                '@type': 'http://schema.org/Review',
                'http://schema.org/reviewRating': {
                    '@type': 'http://schema.org/Rating',
                    'http://schema.org/ratingValue': '" + rating.Value + @"'
                  },
                  'http://schema.org/author': {
                    '@type': 'http://schema.org/Person',
                    'http://schema.org/name': '" + rating.Source + @"'
                  },
                  'http://schema.org/reviewBody': '" + rating.Source + @"'
                },
                'http://schema.org/actor': [";

            docJson = AddActorsArray(movie, docJson);

            docJson += "]}";

            var doc = JObject.Parse(docJson.UnEscapeString());
            var context = JObject.Parse(_movieContextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);

            return compacted;

        }

        private Rating GetRating(MovieDetailResponse movie)
        {
            if (movie.Ratings != null && movie.Ratings.Count > 0)
                return movie.Ratings[0];

            return new Rating();
        }

        private string AddActorsArray(MovieDetailResponse movie, string docJson)
        {
            var isFirst = true;
            var actors = movie.Actors.EscapeString().Split(",");
            foreach (var actor in actors)
            {
                if (isFirst)
                    isFirst = false;
                else
                    docJson += ",";

                docJson += @"
                {
                    '@type': 'http://schema.org/Person',
                    'http://schema.org/name': '" + actor + @"'
                }";
            }

            return docJson;
        }
    }
}
