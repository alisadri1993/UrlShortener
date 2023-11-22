﻿using Shortener.Endpoint.Entities;
using Shortener.Endpoint.Extensions;
using Shortener.Endpoint.Infrastructure.Middleware;
using Shortener.Endpoint.Infrastructure.Repositories;
using System.Text;

namespace Shortener.Endpoint.Services
{
    public class UrlShortenerServeice : IUrlShortenerServeice
    {
        private const int TokenLength = 10;
        private readonly IShortLinkRepository _repository;

        public UrlShortenerServeice(IShortLinkRepository repository)
        {
            _repository = repository;
        }


        public async Task<string> CreateShortLinkAsync(string url)
        {

            if (!url.IsValidUrl()) throw new ShortenerException("url is not valid");
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
            if (shortLink == null) throw new ShortenerException("Token Not Found!");
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
            var token = RandomGenerator.GetRandomString(TokenLength);
            if (_repository.IsExistToken(token)) return GetUniqueRandomString();
            return token;
        }


        
    }
}
