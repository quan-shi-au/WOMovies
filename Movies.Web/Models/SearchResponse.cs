using System.Collections.Generic;

namespace Movies.Web.Models
{
    public class SearchResponse
    {
        public string Title { get; set; }
        public List<MovieResponse> Search { get; set; } = new List<MovieResponse>();
    }
}
