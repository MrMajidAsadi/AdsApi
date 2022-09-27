using Ads.Core.Interfaces;

namespace Ads.Core.Entities;

public record User : BaseEntity, IAggregateRoot
{
    public string IdentityId { get; private set; } = string.Empty;

    public User(string identityId)
    {
        IdentityId = identityId;
    }
}