using Ads.Core.Interfaces;

namespace Ads.Core.Entities;

public record Picture : BaseEntity, IAggregateRoot
{
    public Picture(
        string mimeType,
        string virtualPath,
        int userId,
        string? altAttribute,
        string? titleAttribute)
    {
        MimeType = mimeType;
        VirtualPath = virtualPath;
        UserId = userId;
        AltAttribute = altAttribute;
        TitleAttribute = titleAttribute;
    }


    public int UserId { get; private set; }

    public string MimeType { get; private set; }

    public string VirtualPath { get; private set; }

    public string? AltAttribute { get; private set; }

    public string? TitleAttribute { get; private set; }

    public void UpdateDetails(string mimeType, string virtualPath, string? altAttribute, string? titleAttribute)
    {
        MimeType = mimeType;
        VirtualPath = virtualPath;
        AltAttribute = altAttribute;
        TitleAttribute = titleAttribute;
    }
}