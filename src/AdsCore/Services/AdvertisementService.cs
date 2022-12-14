using Ads.Core.Entities;
using Ads.Core.Entities.AdvertisementAggregate;
using Ads.Core.Interfaces;

namespace Ads.Core.Services;

public class AdvertisementService : IAdvertisementService
{
    private readonly IRepository<AdvertisementCategory> _categoryRepository;

    private readonly IRepository<Picture> _pictureRepository;
    private readonly IRepository<Advertisement> _advertisementRepository;

    public AdvertisementService(
        IRepository<AdvertisementCategory> categoryRepository,
        IRepository<Picture> pictureRepository,
        IRepository<Advertisement> advertisementRepository)
    {
        _categoryRepository = categoryRepository;
        _pictureRepository = pictureRepository;
        _advertisementRepository = advertisementRepository;
    }

    public virtual async Task<Advertisement> Create(
        string title,
        string description,
        int userId,
        string? shortDescription = null,
        IList<int>? categoryIds = null,
        IDictionary<int, bool>? pictureIds = null)
    {
        var advertisement = new Advertisement(title, shortDescription, description, userId);

        if (categoryIds is not null)
            foreach (var categoryId in categoryIds)
            {
                var category = await _categoryRepository.Get(categoryId);
                if (category is null) continue;

                advertisement.AddCategory(category);
            }

        if (pictureIds is not null)
            foreach (var picDic in pictureIds)
            {
                var picture = await _pictureRepository.Get(picDic.Key);
                if (picture is null || picture.UserId != userId) continue;

                advertisement.AddPicture(picture.Id, picDic.Value);
            }

        await _advertisementRepository.Add(advertisement);

        return advertisement;
    }

    public virtual async Task Update(
        Advertisement advertisement,
        string title,
        string description,
        int userId,
        string? shortDescription = null,
        IList<int>? categoryIds = null,
        IDictionary<int, bool>? pictureIds = null)
    {
        advertisement.UpdateDetails(title, shortDescription, description);
        
        if (categoryIds is not null)
        {
            // Deleting deleted categories
            var categories = advertisement.Categories
                .Select(c => c.Id).ToArray();
            foreach (var id in categories)
            {
                var category = advertisement.Categories.SingleOrDefault(c => c.Id == id);
                if (category is not null) advertisement.RemoveCategory(category);
            }

            // Adding new categories
            foreach (var categoryId in categoryIds)
            {
                var category = await _categoryRepository.Get(categoryId);
                if (category is null) continue;

                advertisement.AddCategory(category);
            }
        }

        if (pictureIds is not null)
        {
            var pictures = advertisement.Pictures
                .Select(ap => ap.PictureId).ToArray();
            foreach (var pictureId in pictures)
            {
                if (!pictureIds.Any(pi => pi.Key == pictureId))
                    advertisement.RemovePicture(pictureId);
            }

            foreach (var picDic in pictureIds)
            {
                var picture = await _pictureRepository.Get(picDic.Key);
                if (picture is null || picture.UserId != userId) continue;

                advertisement.AddPicture(picture.Id, picDic.Value);
            }
        }

        await _advertisementRepository.Update(advertisement);
    }

    public virtual async Task Delete(Advertisement advertisement)
    {
        if (advertisement is null) throw new ArgumentNullException(nameof(advertisement));

        advertisement.Delete();
        await _advertisementRepository.Update(advertisement);
    }
}