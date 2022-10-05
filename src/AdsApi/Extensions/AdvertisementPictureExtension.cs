using Ads.Api.Dtos.V1.Pictures;
using Ads.Core.Entities.AdvertisementAggregate;

namespace Ads.Api.Extensions;

public static class AdvertisementPictureExtension
{
    public static AdvertisementPictureDto ToDto(this AdvertisementPicture ap)
    {
        AdvertisementPictureDto output = new()
        {
            Id = ap.PictureId,
            Path = ap.Picture.VirtualPath,
            IsMain = ap.IsMain
        };

        return output;
    }
}