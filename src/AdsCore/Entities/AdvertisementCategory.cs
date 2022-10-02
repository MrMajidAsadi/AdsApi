using Ads.Core.Entities.AdvertisementAggregate;
using Ads.Core.Interfaces;

namespace Ads.Core.Entities;

public record AdvertisementCategory : BaseEntity, IAggregateRoot
{
    public string Name { get; private set; } = string.Empty;

    public string Description { get; private set; } = string.Empty;

    public int ParentCategoryId { get; private set; }

    public int? PictureId { get; private set; }

    public bool Published { get; private set; }

    public bool Deleted { get; private set; }

    public int DisplayOrder { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime UpdatedOnUtc { get; private set; }

    public List<Advertisement> Advertisements { get; private set; }

    public AdvertisementCategory()
    {
        
    }

    public AdvertisementCategory(
        string name,
        string description,
        int parentCategoryId,
        bool published = false,
        bool deleted = false,
        int displayOrder = 0,
        DateTime? createdOnUtc = null,
        DateTime? updatedOnUtc = null,
        int? pictureId = null
    )
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        PictureId = pictureId ?? 0;
        Published = published;
        Deleted = deleted;
        DisplayOrder = displayOrder;

        CreatedOnUtc = createdOnUtc ?? DateTime.UtcNow;
        UpdatedOnUtc = updatedOnUtc ?? DateTime.UtcNow;
    }

    public void UpdateDetails(
        string name,
        string description,
        int parentCategoryId,
        int pictureId,
        int displayOrder = 0)
    {
        Name = name;
        Description = description;
        ParentCategoryId = parentCategoryId;
        PictureId = pictureId;
        DisplayOrder = displayOrder;

        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void Publish()
    {
        Published = true;
        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void Delete()
    {
        Deleted = true;
        UpdatedOnUtc = DateTime.UtcNow;
    }
}