using Microsoft.AspNetCore.Mvc;
using Shortener.Endpoint.Services;

namespace Shortener.Endpoint.Controllers
{
    [ApiController]
    [Route("")]
    public class UrlShortenerController : ControllerBase
    {

        private readonly ILogger<UrlShortenerController> _logger;
        private readonly IUrlShortenerServeice _shortenerServeice;

        public UrlShortenerController(ILogger<UrlShortenerController> logger, IUrlShortenerServeice shortenerServeice)
        {
            _logger = logger;
            _shortenerServeice = shortenerServeice;
        }


        [HttpPost]
        public async Task<string> CreateShortLink(string url)
        {
            return await _shortenerServeice.CreateShortLinkAsync(url);
        }


        [HttpGet("/{token}")]
        public RedirectResult Redirect(string token)
        {
            var url = _shortenerServeice.GetMainUrl(token);
            return new RedirectResult(url);
        }

    }
}