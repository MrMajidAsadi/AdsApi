namespace Ads.Api.Dtos.V1;

public record ErrorDto
{    public List<string> Errors { get; set; } = new();
}