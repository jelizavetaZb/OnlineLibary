using Microsoft.EntityFrameworkCore.Design;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using OnlineLibary.Infrastucture;

namespace OnlineLibary.Migrations.Factory
{
    public class MigrationsDbContextFactory : IDesignTimeDbContextFactory<MigrationsDbContext>
    {
        public MigrationsDbContext CreateDbContext(string[] args)
        {
            var config = new ConfigurationBuilder()
                .SetBasePath(Directory.GetCurrentDirectory())
                .AddJsonFile("appsettings.json")
                .Build();

            var connectionString = config.GetConnectionString(nameof(ApplicationContext));
            var optionsBuilder = new DbContextOptionsBuilder().UseSqlServer(connectionString);

            return new MigrationsDbContext(optionsBuilder.Options);
        }
    }
}
