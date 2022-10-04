using Ads.Api.Dtos.V1.Pictures;
using Ads.Api.Dtos.V1.Users;

namespace Ads.Api.Dtos.V1.Advertisements;

#nullable disable
public class AdvertisementDto
{
    public int Id { get; set; }

    public string Title { get; set; }

    public string Description { get; set; }

    public UserDto User { get; set; }

    public List<PictureDto> PictureUrls { get; set; } = new();
}