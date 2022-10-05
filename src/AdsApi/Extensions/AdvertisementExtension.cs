using Ads.Api.Dtos.V1.Advertisements;
using Ads.Core.Entities;
using Ads.Core.Entities.AdvertisementAggregate;

namespace Ads.Api.Extensions;

public static class AdvertisementExtension
{
    public static AdvertisementDto ToDto(
        this Advertisement advertisement)
    {
        var output =  new AdvertisementDto()
        {
            Id = advertisement.Id,
            Title = advertisement.Title,
            Description = advertisement.Description
        };

        if (advertisement.Pictures is not null)
            foreach (var ap in advertisement.Pictures)
            {
                var pictureDto = ap.ToDto();
                output.PictureUrls.Add(pictureDto);
            }

        if (advertisement.Categories is not null)
            foreach (var category in advertisement.Categories)
            {
                var categoryOverviewDto = category.ToOverviewDto();
                output.Categories.Add(categoryOverviewDto);
            }

        return output;
    }
}