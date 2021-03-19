using System.Collections.Generic;

namespace Movies.Web.Models.Movies
{
    public class SearchResults
    {
        public IList<SearchResult> Results { get; set; }

        //TODO: Delete this. This is just a place holder method to show data displaying
        public void LoadSampleData()
        {
            Results = new List<SearchResult>
            {
                new SearchResult
                {
                    Title = "The Avengers",
                    Year = 2012,
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BNDYxNjQyMjAtNTdiOS00NGYwLWFmNTAtNThmYjU5ZGI2YTI1XkEyXkFqcGdeQXVyMTMxODk2OTU@._V1_SX300.jpg"
                },
                new SearchResult
                {
                    Title = "Avengers: Infinity War",
                    Year = 2018,
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMjMxNjY2MDU1OV5BMl5BanBnXkFtZTgwNzY1MTUwNTM@._V1_SX300.jpg"
                },
                new SearchResult
                {
                    Title = "Avengers: Age of Ultron",
                    Year = 2015,
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTM4OGJmNWMtOTM4Ni00NTE3LTg3MDItZmQxYjc4N2JhNmUxXkEyXkFqcGdeQXVyNTgzMDMzMTg@._V1_SX300.jpg"
                },
                new SearchResult
                {
                    Title = "Avengers: Endgame",
                    Year = 2019,
                    ImageUrl = "https://m.media-amazon.com/images/M/MV5BMTc5MDE2ODcwNV5BMl5BanBnXkFtZTgwMzI2NzQ2NzM@._V1_SX300.jpg"
                },
            }; 
        }

    }
}