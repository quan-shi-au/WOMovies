using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Models
{
    public class SearchResponse
    {
        public List<MovieResponse> Search { get; set; }
    }
}
