namespace Ads.Core.Entities.AdvertisementAggregate;

public record AdvertisementPicture
{
    public int PictureId { get; private set; }
    public Picture Picture { get; private set; }
    public int AdvertisementId { get; private set; }
    public bool IsMain { get; private set; }

    public AdvertisementPicture()
    {
        
    }

    public AdvertisementPicture(int pictureId, bool isMain)
    {
        PictureId = pictureId;
        IsMain = isMain;
    }
}