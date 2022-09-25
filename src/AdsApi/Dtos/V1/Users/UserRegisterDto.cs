using System.ComponentModel.DataAnnotations;

namespace Ads.Api.Dtos.V1.Users;

#nullable disable
public record UserRegisterDto
{
    [Required]
    public string UserName { get; set; }

    [Required]
    public string Password { get; set; }
}