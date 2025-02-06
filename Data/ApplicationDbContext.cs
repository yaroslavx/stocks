using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;
using stocks.Models;

namespace stocks.Data;

public class ApplicationDbContext : IdentityDbContext<AppUser>
{
    public ApplicationDbContext(DbContextOptions options) : base(options)
    {
        
    }
    
    public DbSet<Stock> Stocks { get; set; }
    public DbSet<Comment> Comments { get; set; }
    
    public DbSet<Portfolio> Portfolios { get; set; }

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

        builder.Entity<Portfolio>(x => x.HasKey(p => new { p.AppUserId, p.StockId }));

        builder.Entity<Portfolio>()
            .HasOne(u => u.AppUser)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.AppUserId);
        
        builder.Entity<Portfolio>()
            .HasOne(u => u.Stock)
            .WithMany(u => u.Portfolios)
            .HasForeignKey(p => p.StockId);

        List<IdentityRole> roles = new List<IdentityRole>()
        {
            new IdentityRole
            {
                Id = "2e0f8985-0ef0-47e6-92da-3bb5c4273fa5",
                Name = "Admin",
                NormalizedName = "Admin".ToUpper(),
                ConcurrencyStamp = "2e0f8985-0ef0-47e6-92da-3bb5c4273fa5"
            },

            new IdentityRole
            {
                Id = "57dec208-734e-4ca6-a270-d05687f4df81",
                Name = "User",
                NormalizedName = "User".ToUpper(),
                ConcurrencyStamp = "57dec208-734e-4ca6-a270-d05687f4df81"
            },
        };
        builder.Entity<IdentityRole>().HasData(roles);
    }
}