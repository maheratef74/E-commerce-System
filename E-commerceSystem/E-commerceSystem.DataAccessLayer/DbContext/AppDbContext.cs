using E_commerceSystem.DataAccessLayer.Entities;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore;

namespace E_commerceSystem.DataAccessLayer.DbContext;

public class AppDbContext:Microsoft.EntityFrameworkCore.DbContext 
{
    public AppDbContext(DbContextOptions<AppDbContext> options) : base(options)
    {
    }
    
    public DbSet<User?> Users { get; set; } = null;
    public DbSet<Product> Products { get; set; } = null!;
}