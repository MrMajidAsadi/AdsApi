using System.ComponentModel.DataAnnotations;

namespace Ads.Api.Dtos.V1.Pictures;

public class UpdatePictureDto
{
    [Required]
    public int Id { get; set; }

    [Required]
    [MaxLength(50)]
    public string MimeType { get; set; } = string.Empty;

    [Required]
    [MaxLength(150)]
    public string VirtualPath { get; set; } = string.Empty;
    
    public string? AltAttribute { get; set; }

    public string? TitleAttribute { get; set; }
}