namespace Ads.Core.Entities;

public record Picture : BaseEntity
{
    public Picture(
        string mimeType,
        string virtualPath,
        string? altAttribute,
        string? titleAttribute)
    {
        MimeType = mimeType;
        VirtualPath = virtualPath;
        AltAttribute = altAttribute;
        TitleAttribute = titleAttribute;
    }

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