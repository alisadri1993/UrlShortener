using Microsoft.EntityFrameworkCore;
using Shortener.Endpoint.Infrastructure.DataAccess;
using Shortener.Endpoint.Infrastructure.Repositories;
using Shortener.Endpoint.Services;

namespace Shortener.Endpoint.Extensions
{
    public static class DependencyInjection
    {
        public static void AddUrlShortenerApp(this IServiceCollection services, IConfiguration configuration)
        {
            services.AddScoped<IShortLinkRepository, ShortLinkRepository>();
            services.AddScoped<IUrlShortenerServeice, UrlShortenerServeice>();

            services.AddDbContext<UrlShortenerDbContext>(options =>
            options.UseSqlServer(configuration.GetConnectionString("DatabaseConnection")));

            services.AddControllers();
            services.AddEndpointsApiExplorer();
            services.AddSwaggerGen();

        }
    }
}
