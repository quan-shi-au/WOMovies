using Microsoft.Extensions.DependencyInjection;
using Movies.Web.Services;
using Movies.Web.Services.Interfaces;

namespace Movies.Web.Infrastructure
{
    public static class ServiceExtension
    {
        public static void RegisterDi(this IServiceCollection services)
        {
            services.AddScoped<IMovieApiService, MovieApiService>();
            services.AddScoped<IJsonLdService, JsonLdService>();
        }

    }
}
