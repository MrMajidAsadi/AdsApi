namespace Ads.Api.Dtos.V1.Users;

public record UserLoginResponseDto
{
    public string Token { get; set; } = string.Empty;

    public UserDto User { get; set; } = new();
}