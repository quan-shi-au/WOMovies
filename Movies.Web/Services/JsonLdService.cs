using JsonLD.Core;
using Movies.Web.Infrastructure;
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
        public JObject GetMoviesJObject1(List<MovieResponse> movies)
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
                    'http://schema.org/name': '" + movie.Title.EscapeString() + @"',
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

        public JObject GetMoviesJObject(List<MovieResponse> movies)
        {
            var _docJson = @"
{
        '@id': 'http://example.org/movies',
        'http://schema.org/name': 'Movies',
        '@type': 'http://schema.org/ItemList',
 'http://schema.org/itemListElement': [";

            var _contextJson = @"
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
}
";

            var position = 1;
            foreach (var movie in movies)
            {
                if (position > 1)
                    _docJson += ",";

                _docJson += @"
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
        }
";
                position++;
            }

            _docJson += "]}";



            var doc = JObject.Parse(_docJson);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);


            return compacted;

        }



        public JObject GetMovieDetailObject1(MovieDetailResponse movie)
        {
            var _docJson = @"
{
    '@id': '" + movie.imdbID + @"',
        '@type': 'Movie Detail',
    'http://schema.org/Title': '" + movie.Title.EscapeString() + @"',
    'http://schema.org/Year': '" + movie.Year.EscapeString() + @"',
    'http://schema.org/Rated': '" + movie.Rated.EscapeString() + @"',
    'http://schema.org/Released': '" + movie.Released.EscapeString() + @"',
    'http://schema.org/Runtime': '" + movie.Runtime.EscapeString() + @"',
    'http://schema.org/Genre': '" + movie.Genre.EscapeString() + @"',
    'http://schema.org/Director': '" + movie.Director.EscapeString() + @"',
    'http://schema.org/Writer': '" + movie.Writer.EscapeString() + @"',
    'http://schema.org/Actors': '" + movie.Actors.EscapeString() + @"',
    'http://schema.org/Plot': '" + movie.Plot.EscapeString() + @"',
    'http://schema.org/Language': '" + movie.Language.EscapeString() + @"',
    'http://schema.org/Country': '" + movie.Country.EscapeString() + @"',
    'http://schema.org/Awards': '" + movie.Awards.EscapeString() + @"',
    'http://schema.org/Poster': '" + movie.Poster.EscapeString() + @"',
    'http://schema.org/Metascore': '" + movie.Metascore.EscapeString() + @"',
    'http://schema.org/imdbRating': '" + movie.imdbRating.EscapeString() + @"',
    'http://schema.org/imdbVotes': '" + movie.imdbVotes.EscapeString() + @"',
    'http://schema.org/imdbID': '" + movie.imdbID.EscapeString() + @"',
    'http://schema.org/DVD': '" + movie.DVD.EscapeString() + @"',
    'http://schema.org/BoxOffice': '" + movie.BoxOffice.EscapeString() + @"',
    'http://schema.org/Production': '" + movie.Production.EscapeString() + @"',
    'http://schema.org/Website': '" + movie.Website.EscapeString() + @"',
    'http://schema.org/Response': '" + movie.Response.EscapeString() + @"',
    'http://schema.org/Ratings':[
";

            var isFirst = true;
            foreach (var rating in movie.Ratings)
            {
                if (isFirst)
                    isFirst = false;
                else
                    _docJson += ",";

                _docJson += @"
                {
                    'http://schema.org/Source': '" + rating.Source + @"',
                    'http://schema.org/Value': '" + rating.Value + @"'
                }
                ";
            }

            _docJson += "]}";



            var _contextJson = @"
{
    'Title': 'http://schema.org/Title',
    'Year': 'http://schema.org/Year',
    'Rated': 'http://schema.org/Rated',
    'Released': 'http://schema.org/Released',
    'Runtime': 'http://schema.org/Runtime',
    'Genre': 'http://schema.org/Genre',
    'Director': 'http://schema.org/Director',
    'Writer': 'http://schema.org/Writer',
    'Actors': 'http://schema.org/Actors',
    'Plot': 'http://schema.org/Plot',
    'Language': 'http://schema.org/Language',
    'Country': 'http://schema.org/Country',
    'Awards': 'http://schema.org/Awards',
    'Poster': 'http://schema.org/Poster',
    'Metascore': 'http://schema.org/Metascore',
    'imdbRating': 'http://schema.org/imdbRating',
    'imdbVotes': 'http://schema.org/imdbVotes',
    'imdbID': 'http://schema.org/imdbID',
    'DVD': 'http://schema.org/DVD',
    'BoxOffice': 'http://schema.org/BoxOffice',
    'Production': 'http://schema.org/Production',
    'Website': 'http://schema.org/Website',
    'Response': 'http://schema.org/Response',
    'Ratings': 'http://schema.org/Ratings',
    'Source': 'http://schema.org/Source',
    'Value': 'http://schema.org/Value'
}";

            var doc = JObject.Parse(_docJson);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);


            return compacted;

        }


        public JObject GetMovieDetailObject(MovieDetailResponse movie)
        {
            var _docJson = @"
{
        '@id': 'http://example.org/movies',
        'http://schema.org/name': '" + movie.Title.EscapeString() + @"',
        '@type': 'http://schema.org/Movie',
        'http://schema.org/image': {'@id': '" + movie.Poster + @"'},
'http://schema.org/dateCreated' : '" + movie.Year.ValidateYear() + @"',
'http://schema.org/director': '" + movie.Director + @"',
'http://schema.org/review': {
    '@type': 'http://schema.org/Review',
    'http://schema.org/reviewRating': {
                '@type': 'http://schema.org/Rating',
                'http://schema.org/ratingValue': '" + movie.Ratings[0].Value + @"'
              },
              'http://schema.org/author': {
                '@type': 'http://schema.org/Person',
                'http://schema.org/name': '" + movie.Ratings[0].Source + @"'
              },
              'http://schema.org/reviewBody': '" + movie.Ratings[0].Source + @"'
},
'http://schema.org/actor': [";


            var isFirst = true;
            var actors = movie.Actors.Split(",");
            foreach (var actor in actors)
            {
                if (isFirst)
                    isFirst = false;
                else
                    _docJson += ",";

                _docJson += @"

                {
                    '@type': 'http://schema.org/Person',
                    'http://schema.org/name': '" + actor + @"'
                }
            ";
            }

            _docJson += "]}";



            var _contextJson = @"
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

            var doc = JObject.Parse(_docJson);
            var context = JObject.Parse(_contextJson);
            var opts = new JsonLdOptions();
            var compacted = JsonLdProcessor.Compact(doc, context, opts);


            return compacted;

        }

    }
}
