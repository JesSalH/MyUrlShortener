namespace MyUrlShortener.Services.UrlShortener
{
    public class UrlShortenerServiceException : Exception
    {
        public UrlShortenerServiceException(string? message) : base(message) {}
    }
}
