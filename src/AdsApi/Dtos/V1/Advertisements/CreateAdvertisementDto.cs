using System.ComponentModel.DataAnnotations;

namespace Ads.Api.Dtos.V1.Advertisements;

public class CreateAdvertisementDto
{
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? ShortDescription { get; set; }

    public string Description { get; set; } = string.Empty;

    public List<int>? CategoryIds { get; set; }

    public List<CreateAdvertisementPictureDto>? Pictures { get; set; } = new();
}