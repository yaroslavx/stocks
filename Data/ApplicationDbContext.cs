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

    protected override void OnModelCreating(ModelBuilder builder)
    {
        base.OnModelCreating(builder);

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