using Ads.Api.Dtos.V1.Pictures;
using Ads.Core.Entities.AdvertisementAggregate;

namespace Ads.Api.Extensions;

public static class AdvertisementPictureExtension
{
    public static PictureDto ToDto(this AdvertisementPicture ap)
    {
        PictureDto output = new()
        {
            Id = ap.PictureId,
            Path = ap.Picture.VirtualPath,
            IsMain = ap.IsMain
        };

        return output;
    }
}