using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using MyUrlShortener.Models;

namespace MyUrlShortener.DataAccess.Data.Configurations.ShortenedUrls
{
    internal sealed class ShortenedUrlConfiguration : IEntityTypeConfiguration<ShortenedUrl>
    {
        public void Configure(EntityTypeBuilder<ShortenedUrl> builder)
        {
            builder.HasKey(s => s.Id);
            builder.HasIndex(s => s.Code).IsUnique();
            builder.Property(s => s.Code).IsRequired();
            builder.HasIndex(s => s.OriginalUrl).IsUnique();
            builder.Property(s => s.OriginalUrl).IsRequired();
            builder.Property(s => s.Code).HasMaxLength(32);
        }
    }
}
