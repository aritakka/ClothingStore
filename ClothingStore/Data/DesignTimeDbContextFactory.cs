using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Design;

namespace ClothingStore.Data;

public class DesignTimeDbContextFactory : IDesignTimeDbContextFactory<AppDbContext>
{
    public AppDbContext CreateDbContext(string[] args)
    {
        var optionsBuilder = new DbContextOptionsBuilder<AppDbContext>();
        var conn = "Server=(localdb)\\mssqllocaldb;Database=ClothingStoreDb;Trusted_Connection=True;";
        optionsBuilder.UseSqlServer(conn);
        return new AppDbContext(optionsBuilder.Options);
    }
}
