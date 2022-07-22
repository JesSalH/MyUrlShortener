using MyUrlShortener.Models;

namespace MyUrlShortener.DataAccess.Repositories.IRepositories
{
    public interface IShortenedUrlRepository : IRepository<ShortenedUrl>
    {
        void UpdateOriginalUrl(ShortenedUrl shortenedUrlObj);
    }
}
