using Shortener.Endpoint.Entities;

namespace Shortener.Endpoint.Infrastructure.Repositories
{
    public interface IShortLinkRepository
    {
        Task<int> AddShortLinkAsync(ShortLink shortLink);
        ShortLink? GetShortLinkByUrl(string url);
        ShortLink? GetShortLinkByToken(string token);
    }
}
