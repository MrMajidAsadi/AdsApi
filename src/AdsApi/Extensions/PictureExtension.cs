using Ads.Api.Dtos.V1.Pictures;
using Ads.Core.Entities;

namespace Ads.Api.Extensions;

public static class PictureExtension
{
    public static PictureDto ToDto(this Picture picture)
    {
        PictureDto output = new()
        {
            Id = picture.Id,
            Path = picture.VirtualPath
        };

        return output;
    }
}