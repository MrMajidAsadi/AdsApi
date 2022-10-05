using Ads.Api.Dtos.V1.Advertisements;
using Ads.Core.Entities;

namespace Ads.Api.Extensions;

public static class AdvertisementCategoryExtension
{
    public static AdvertisementCategoryOverviewDto ToOverviewDto(this AdvertisementCategory advertisementCategory)
    {
        AdvertisementCategoryOverviewDto output = new()
        {
            Id = advertisementCategory.Id,
            Name = advertisementCategory.Name
        };

        return output;
    }
}