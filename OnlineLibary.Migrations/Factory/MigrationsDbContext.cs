using Microsoft.EntityFrameworkCore;
using OnlineLibary.Infrastructure;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Migrations.Factory
{
    public class MigrationsDbContext : DbContext
    {
        public MigrationsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            // Explicitly set the Provider Name in order to switch correct database specifics (sql statements)
            EntityTypeBuilderExtensions.ProviderName = Database.ProviderName;
            // Apply all configurations
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(UserIdentityDbContext).Assembly);
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationDbContext).Assembly);

            // Pluralizes all table names
            modelBuilder.PluralizeTableNames();
            // Disable cascade delete globally
            modelBuilder.DisableCascadeDelete();
        }
    }
}
