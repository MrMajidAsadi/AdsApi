using Ads.Core.Entities.AdvertisementAggregate;

namespace Ads.Core.Interfaces;

public interface IAdvertisementService
{
    Task<Advertisement> Create(string title, string description, int userId, string? shortDescription = null, IList<int>? categoryIds = null, IDictionary<int, bool>? pictureIds = null);

    Task Delete(Advertisement advertisement);
}
