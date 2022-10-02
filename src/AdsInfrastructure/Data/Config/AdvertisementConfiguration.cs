using Ads.Core.Entities;
using Ads.Core.Entities.AdvertisementAggregate;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ads.Infrastructure.Data.Config;

public class AdvertisementConfiguration : IEntityTypeConfiguration<Advertisement>
{
    public void Configure(EntityTypeBuilder<Advertisement> builder)
    {
        var pictureNavigation = builder.Metadata.FindNavigation(nameof(Advertisement.Pictures));
        pictureNavigation.SetPropertyAccessMode(PropertyAccessMode.Field);

        builder.Property(a => a.Title)
            .HasMaxLength(100);
        
        builder.Property(p => p.ShortDescription)
            .HasMaxLength(200);
        
        builder.Property(p => p.Description)
            .HasMaxLength(500);

    }
}
