namespace Shortener.Endpoint.Services
{
    public interface IUrlShortenerServeice
    {
        public Task<string> CreateShortLinkAsync(string url);
        public string GetMainUrl(string token);
    }
}
