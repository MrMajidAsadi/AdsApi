using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace Ads.Infrastructure.Identity;

public class AdsAppDbContext : IdentityDbContext<AdsAppUser>
{
    public AdsAppDbContext(DbContextOptions<AdsAppDbContext> options)
        : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);
        
        builder.Entity<AdsAppUser>().Property(u => u.FirstName)
            .HasMaxLength(50);
        builder.Entity<AdsAppUser>().Property(u => u.LastName)
            .HasMaxLength(50);
    }
}