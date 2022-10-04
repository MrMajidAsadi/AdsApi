using Ads.Api.Dtos.V1.Advertisements;
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

        foreach (var ap in advertisement.Pictures)
        {
            var pictureDto = ap.ToDto();
            output.PictureUrls.Add(pictureDto);
        }

        return output;
    }
}