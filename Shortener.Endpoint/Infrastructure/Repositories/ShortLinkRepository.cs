using Shortener.Endpoint.Entities;
using Shortener.Endpoint.Infrastructure.DataAccess;

namespace Shortener.Endpoint.Infrastructure.Repositories
{
    public class ShortLinkRepository : IShortLinkRepository
    {
        private readonly UrlShortenerDbContext _dbContext;

        public ShortLinkRepository(UrlShortenerDbContext dbContext)
        {
            _dbContext = dbContext;
        }
        public async Task<int> AddShortLinkAsync(ShortLink shortLink)
        {
            await _dbContext.shortLinks.AddAsync(shortLink);
            await _dbContext.SaveChangesAsync();
            return shortLink.Id;
        }

        public ShortLink? GetShortLinkByToken(string token)
        {
            return _dbContext.shortLinks.FirstOrDefault(s => s.Token == token);
        }

        public ShortLink? GetShortLinkByUrl(string url)
        {
            return _dbContext.shortLinks.FirstOrDefault(s => s.Url == url);
        }
    }
}
