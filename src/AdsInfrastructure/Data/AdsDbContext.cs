using System.Reflection;
using Ads.Core.Entities;
using Ads.Core.Entities.AdvertisementAggregate;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Data;

#nullable disable
public class AdsDbContext : DbContext
{
    public AdsDbContext(DbContextOptions<AdsDbContext> options)
        : base(options)
    {
    }

    public DbSet<User> Users { get; set; }
    public DbSet<Picture> pictures { get; set; }
    public DbSet<Advertisement> Advertisements { get; set; }
    public DbSet<AdvertisementPicture> AdvertisementPictures { get; set; }
    public DbSet<AdvertisementCategory> AdvertisementCategories { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}