using E_commerceSystem.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSystem.DataAccessLayer.DbContext;

public class AppDbContext :IdentityDbContext<User>
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }

    protected override void OnModelCreating(ModelBuilder modelBuilder)
    {
        base.OnModelCreating(modelBuilder);

        // change names of tables that identity make it 
        modelBuilder.Entity<User>(e => e.ToTable("Users"));
        modelBuilder.Entity<IdentityRole>(e => e.ToTable("Roles"));
        modelBuilder.Entity<IdentityUserRole<string>>(e => e.ToTable("UserRoles"));
        modelBuilder.Entity<IdentityUserClaim<string>>(e => e.ToTable("UserClaims"));
        modelBuilder.Entity<IdentityUserLogin<string>>(e => e.ToTable("UserLogins"));
        modelBuilder.Entity<IdentityRoleClaim<string>>(e => e.ToTable("RoleCliams"));
        modelBuilder.Entity<IdentityUserToken<string>>(e => e.ToTable("UserTokens"));

        
    }
    
}