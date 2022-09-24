using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Identity;

public class AdsAppIdentityDbContextSeed
{
    public static async Task Seed(
        AdsAppDbContext identityDbContext,
        UserManager<AdsAppUser> userManager,
        RoleManager<IdentityRole> roleManager)
    {
        if (identityDbContext.Database.IsSqlServer())
        {
            identityDbContext.Database.Migrate();
        }

        await roleManager.CreateAsync(new IdentityRole(Ads.Core.Constants.Authorization.Roles.ADMINISTRATORS));

        var defaultUser = new AdsAppUser { UserName = "demouser@microsoft.com", Email = "demouser@microsoft.com" };
        await userManager.CreateAsync(defaultUser, Ads.Core.Constants.Authorization.DEFAULT_PASSWORD);

        string adminUserName = "admin@microsoft.com";
        var adminUser = new AdsAppUser { UserName = adminUserName, Email = adminUserName };
        await userManager.CreateAsync(adminUser, Ads.Core.Constants.Authorization.DEFAULT_PASSWORD);
        adminUser = await userManager.FindByNameAsync(adminUserName);
        await userManager.AddToRoleAsync(adminUser, Ads.Core.Constants.Authorization.Roles.ADMINISTRATORS);
    
    }
}