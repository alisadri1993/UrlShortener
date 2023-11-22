using Shortener.Endpoint.Entities;

namespace Shortener.Endpoint.Infrastructure.Repositories
{
    public interface IShortLinkRepository
    {
        Task<int> AddShortLinkAsync(ShortLink shortLink);
        void UpdateShortLink(ShortLink shortLink);
        ShortLink? GetShortLinkByUrl(string url);
        ShortLink? GetShortLinkByToken(string token);
        bool IsExistToken(string token);
    }
}
