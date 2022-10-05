using Ads.Core.Interfaces;

namespace Ads.Core.Entities.AdvertisementAggregate;

public record Advertisement : BaseEntity, IAggregateRoot
{
    public string Title { get; private set; }

    public string? ShortDescription { get; private set; }

    public string Description { get; private set; } 

    public int UserId { get; private set; }

    public bool Published { get; private set; }
    
    public bool Deleted { get; private set; }

    public DateTime CreatedOnUtc { get; private set; }

    public DateTime UpdatedOnUtc { get; private set; }

    public User User { get; private set; }

    private readonly List<AdvertisementPicture> _pictures;
    public IReadOnlyCollection<AdvertisementPicture> Pictures => _pictures.AsReadOnly();

    private readonly List<AdvertisementCategory> _categories;
    public IReadOnlyCollection<AdvertisementCategory> Categories => _categories.AsReadOnly();

    public Advertisement()
    {
        
    }

    public Advertisement(
        string title,
        string? shortDescription,
        string description,
        int userId,
        bool published = false,
        bool deleted = false,
        DateTime? createdOnUtc = null,
        DateTime? updatedOnUtc = null,
        List<AdvertisementPicture>? pictures = null,
        List<AdvertisementCategory>? categories = null)
    {
        Title = title;
        ShortDescription = shortDescription;
        Description = description;
        UserId = userId;

        Published = published;
        Deleted = deleted;
        CreatedOnUtc = createdOnUtc ?? DateTime.UtcNow;
        UpdatedOnUtc = updatedOnUtc ?? DateTime.UtcNow;
        _pictures = pictures ?? new();
        _categories = categories ?? new();
    }

    public void UpdateDetails(string title, string? shortDescription, string description)
    {
        if (string.IsNullOrEmpty(title)) throw new ArgumentNullException(nameof(title));
        if (string.IsNullOrEmpty(shortDescription)) throw new ArgumentNullException(nameof(shortDescription));
        if (string.IsNullOrEmpty(description)) throw new ArgumentNullException(nameof(description));
        
        Title = title;
        ShortDescription = shortDescription;
        Description = description;

        UpdatedOnUtc = DateTime.UtcNow;
    }

    public void AddPicture(int pictureId, bool isMain)
    {
        var existingPicture = Pictures.SingleOrDefault(p => p.PictureId == pictureId);

        if (existingPicture is not null) return;

        if (isMain)
        {
            var prevMainPic = _pictures.SingleOrDefault(p => p.IsMain);
            if (prevMainPic is not null)
                prevMainPic.UpdateDetails(false);
        }

        _pictures.Add(new AdvertisementPicture(pictureId, isMain));    
    }

    public void RemovePicture(int pictureId)
    {
        var existingPicture = Pictures.SingleOrDefault(p => p.PictureId == pictureId);

        if (existingPicture is not null) _pictures.Remove(existingPicture);
    }

    public void AddCategory(AdvertisementCategory category)
    {
        if (category is null) throw new ArgumentNullException(nameof(category));
        var existingCategory = Categories.SingleOrDefault(c => c.Id == category.Id);

        if (existingCategory is not null) return;

        _categories.Add(category);
    }

    public void RemoveCategory(AdvertisementCategory category)
    {
        if (category is null) throw new ArgumentNullException(nameof(category));
        var existingCategory = Categories.SingleOrDefault(c => c.Id == category.Id);

        if (existingCategory is null) return;

        _categories.Remove(existingCategory);
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