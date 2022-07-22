using EnsureThat;

namespace MyUrlShortener.Models
{
    public sealed class ShortenedUrl
    {
        public ShortenedUrl(string originalUrl, string code)
        {
            EnsureArg.IsNotNullOrWhiteSpace(originalUrl, nameof(originalUrl));
            EnsureArg.IsNotNullOrWhiteSpace(code, nameof(code));

            OriginalUrl = originalUrl;
            Code = code;
        }


        //for EF
#pragma warning disable CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        private ShortenedUrl()
#pragma warning restore CS8618 // Non-nullable field must contain a non-null value when exiting constructor. Consider declaring as nullable.
        {
        }

        public int Id { get; set; }

        public string OriginalUrl { get; set; }

        public string Code { get; private set; }
    }
}