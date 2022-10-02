using Ads.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ads.Infrastructure.Data.Config;

public class PictureConfiguration : IEntityTypeConfiguration<Picture>
{
    public void Configure(EntityTypeBuilder<Picture> builder)
    {
        builder.Property(p => p.MimeType)
            .HasMaxLength(20);
        
        builder.Property(p => p.VirtualPath)
            .HasMaxLength(150);

        builder.Property(p => p.AltAttribute)
            .HasMaxLength(100);

        builder.Property(p => p.TitleAttribute)
            .HasMaxLength(200);
    }
}