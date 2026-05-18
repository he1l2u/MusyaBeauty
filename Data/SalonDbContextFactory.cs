using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace BeautySalonApp.Data;

public class SalonDbContextFactory : IDesignTimeDbContextFactory<SalonDbContext>
{
    public SalonDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<SalonDbContext>();
        optionsBuilder.UseNpgsql("Host=localhost;Port=5432;Database=beautysalon_db;Username=beauty_user;Password=beauty_password");
        return new SalonDbContext(optionsBuilder.Options);
    }
}
