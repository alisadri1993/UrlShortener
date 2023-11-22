using Microsoft.EntityFrameworkCore;
using Shortener.Endpoint.Entities;
using Shortener.Endpoint.Entities.TypeConfigurations;

namespace Shortener.Endpoint.Infrastructure.DataAccess
{
    public class UrlShortenerDbContext : DbContext
    {
        public UrlShortenerDbContext(DbContextOptions<UrlShortenerDbContext> options)
            : base(options)
        {

        }

        public DbSet<ShortLink> shortLinks { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            base.OnModelCreating(modelBuilder);

            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ShortLinkConfiguration).Assembly);
        }
    }
}
