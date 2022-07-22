using MyUrlShortener.DataAccess.Data;
using MyUrlShortener.DataAccess.Repositories.IRepositories;
using MyUrlShortener.Models;

namespace MyUrlShortener.DataAccess.Repositories
{
    public sealed class ShortenedUrlRepository : Repository<ShortenedUrl>, IShortenedUrlRepository
    {
        public ShortenedUrlRepository(ApplicationDbContext db)
            :base(db)
        {
        }

        public void UpdateOriginalUrl(ShortenedUrl shortenedUrlObj)
        {
            var objFromDb = _db.ShortenedUrls.FirstOrDefault(s => s.Id == shortenedUrlObj.Id);
            objFromDb!.OriginalUrl = shortenedUrlObj.OriginalUrl;
            _db.SaveChanges();
        }
    }
}
