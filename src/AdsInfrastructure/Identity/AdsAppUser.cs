using Microsoft.AspNetCore.Identity;

namespace Ads.Infrastructure.Identity;

public class AdsAppUser : IdentityUser
{
    public string? FirstName { get; set; }

    public string? LastName { get; set; }
}