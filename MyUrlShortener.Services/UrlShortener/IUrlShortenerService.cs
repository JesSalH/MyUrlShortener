namespace MyUrlShortener.Services.UrlShortener
{
    public interface IUrlShortenerService
    {
        string ShortenUrl(string url);

        string GetOriginalUrl(string shortenedUrl);
    }
}
