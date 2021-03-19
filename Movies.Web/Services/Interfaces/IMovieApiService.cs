using Movies.Web.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Movies.Web.Services.Interfaces
{
    public interface IMovieApiService
    {
        Task<SearchResponse> SearchByTitle(string title);
        Task<MovieDetailResponse> GetDetail(string imdbId);
    }
}
