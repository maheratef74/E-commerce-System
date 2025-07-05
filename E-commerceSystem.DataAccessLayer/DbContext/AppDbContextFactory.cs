using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;
using Microsoft.Extensions.Configuration;

namespace E_commerceSystem.DataAccessLayer.DbContext;

public class AppDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();

        optionsBuilder.UseSqlServer("Server=db22847.public.databaseasp.net; Database=db22847; User Id=db22847; Password=3Ah#C-7n+xB5; Encrypt=True; TrustServerCertificate=True; MultipleActiveResultSets=True;");
        return new AppDbContext(optionsBuilder.Options);
    }
}
