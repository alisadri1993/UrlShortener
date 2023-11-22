namespace Shortener.Endpoint.Infrastructure.Middleware
{
    public class ShortenerException : Exception
    {
        public ShortenerException(string msg) : base(msg)
        {

        }
    }
}
