using Ads.Core.Entities;
using Ads.Infrastructure.Identity;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Data;

public class AdsDbContextSeed
{
    public static async Task Seed(
        AdsDbContext adsDbContext,
        AdsAppDbContext adsAppDbContext)
    {
        if (adsDbContext.Database.IsSqlServer())
            await adsDbContext.Database.MigrateAsync();

        if (await adsDbContext.Users.CountAsync() > 0)
            return;
            
        var users = await adsAppDbContext.Users.ToListAsync();

        foreach (var user in users)
        {
            await adsDbContext.Users.AddAsync(new User(user.Id));
            await adsDbContext.SaveChangesAsync();
        }
    }
}