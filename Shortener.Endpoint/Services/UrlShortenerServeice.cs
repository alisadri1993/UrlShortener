using Shortener.Endpoint.Entities;
using Shortener.Endpoint.Infrastructure.Repositories;
using System.Text;

namespace Shortener.Endpoint.Services
{
    public class UrlShortenerServeice : IUrlShortenerServeice
    {
        private readonly IShortLinkRepository _repository;

        public UrlShortenerServeice(IShortLinkRepository repository)
        {
            _repository = repository;
        }


        public async Task<string> CreateShortLinkAsync(string url)
        {
            var existedShortLink = _repository.GetShortLinkByUrl(url);
            if (existedShortLink != null)
            {
                return existedShortLink.Token;
            }
            else
            {
                var token = GetUniqueRandomString();
                var shortener = new ShortLink
                {
                    Url = url,
                    Token = token,
                    CreateAt = DateTime.Now,
                };
                await _repository.AddShortLinkAsync(shortener);

                return token;
            }
        }

        public string GetMainUrl(string token)
        {
            var shortLink = _repository.GetShortLinkByToken(token);
            if (shortLink == null) return "Token Not Found!";
            {
                AddUrlVisited(shortLink.Token);
                return shortLink.Url;
            }
        }

        private void AddUrlVisited(string token)
        {
            var shortLink = _repository.GetShortLinkByToken(token);
            if (shortLink != null)
            {
                shortLink.ReadCount++;
                _repository.UpdateShortLink(shortLink);
            }
        }

        private string GetUniqueRandomString()
        {
            var token = GetRandomString(10);
            if (_repository.IsExistToken(token)) return GetUniqueRandomString();
            return token;
        }


        public static string GetRandomString(int stringLength)
        {
            StringBuilder sb = new StringBuilder();
            int numGuidsToConcat = (((stringLength - 1) / 32) + 1);
            for (int i = 1; i <= numGuidsToConcat; i++)
            {
                sb.Append(Guid.NewGuid().ToString("N"));
            }

            return sb.ToString(0, stringLength);
        }
    }
}
