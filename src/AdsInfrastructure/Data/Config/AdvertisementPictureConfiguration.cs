using Ads.Core.Entities;
using Ads.Core.Entities.AdvertisementAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Data.Config;

public class AdvertisementPictureConfiguration : IEntityTypeConfiguration<AdvertisementPicture>
{
    public void Configure(Microsoft.EntityFrameworkCore.Metadata.Builders.EntityTypeBuilder<AdvertisementPicture> builder)
    {
        builder.HasKey(nameof(AdvertisementPicture.PictureId), nameof(AdvertisementPicture.AdvertisementId));
        
        builder.HasOne(p => p.Picture)
            .WithMany()
            .HasForeignKey(p => p.PictureId);
    }
}
