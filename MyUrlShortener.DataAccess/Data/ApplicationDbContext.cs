using Microsoft.EntityFrameworkCore;
using MyUrlShortener.DataAccess.Data.Configurations.ShortenedUrls;
using MyUrlShortener.Models;

namespace MyUrlShortener.DataAccess.Data
{
    public sealed class ApplicationDbContext : DbContext
    {
        public ApplicationDbContext(DbContextOptions<ApplicationDbContext> options)
            : base(options)
        {
        }

        public DbSet<ShortenedUrl> ShortenedUrls { get; set; }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfiguration(new ShortenedUrlConfiguration());
        }
    }
}
