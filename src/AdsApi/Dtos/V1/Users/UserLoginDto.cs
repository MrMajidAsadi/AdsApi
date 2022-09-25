using System.ComponentModel.DataAnnotations;

namespace Ads.Api.Dtos.V1.Users;

public record UserLoginDto
{
    [Required]
    public string UserName { get; set; } = string.Empty;

    [Required]
    public string Password { get; set; } = string.Empty;

    public bool RememberMe { get; set; }
}