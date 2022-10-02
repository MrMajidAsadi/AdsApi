using Ads.Core.Entities;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;

namespace Ads.Infrastructure.Data.Config;

public class AdvertisementCategoryConfiguration : IEntityTypeConfiguration<AdvertisementCategory>
{
    public void Configure(EntityTypeBuilder<AdvertisementCategory> builder)
    {
        builder.Property(ac => ac.Name)
            .HasMaxLength(100);

        builder.Property(ac => ac.Description)
            .HasMaxLength(500);
    }
}
