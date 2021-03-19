using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models
{
    public class SearchResponse
    {
        public string Title { get; set; }
        public List<MovieResponse> Search { get; set; } = new List<MovieResponse>();
    }
}
