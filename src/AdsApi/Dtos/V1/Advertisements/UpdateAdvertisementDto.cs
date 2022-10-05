using System.ComponentModel.DataAnnotations;

namespace Ads.Api.Dtos.V1.Advertisements;

public class UpdateAdvertisementDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(100)]
    public string Title { get; set; } = string.Empty;

    [MaxLength(200)]
    public string? ShortDescription { get; set; }

    [Required]
    [MaxLength(500)]
    public string Description { get; set; } = string.Empty;

    public List<int>? CategoryIds { get; set; } = new();

    public List<CreateAdvertisementPictureDto>? Pictures { get; set; } = new();
}