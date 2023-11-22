namespace Shortener.Endpoint.Entities
{
    public class ShortLink
    {
        public int Id { get; set; }
        public int ReadCount { get; set; }
        public string Url { get; set; }
        public string Token { get; set; }
        public DateTime CreateAt { get; set; }
    }
}
