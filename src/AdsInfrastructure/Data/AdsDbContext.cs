using System.Reflection;
using Ads.Core.Entities;
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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        builder.ApplyConfigurationsFromAssembly(Assembly.GetExecutingAssembly());
    }
}