using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;
using OnlineLibary.Infrastucture;

namespace OnlineLibary.Migrations.Factory
{
    public class MigrationsDbContext : DbContext
    {
        public MigrationsDbContext(DbContextOptions options) : base(options) { }

        protected override void OnModelCreating(ModelBuilder modelBuilder)
        {
            modelBuilder.ApplyConfigurationsFromAssembly(typeof(ApplicationContext).Assembly);
            PluralizeTableNames(modelBuilder);
        }

        /// <summary>
        /// Pluralizes all table names, except where <c>.ToTable</c> is explicitly set
        /// </summary>
        private void PluralizeTableNames(ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // Ignore if ToTable (TableName annotation) is explicitly set
                if (entityType.FindAnnotation(RelationalAnnotationNames.TableName) != null) continue;

                var pluralTableName = entityType.DisplayName().Pluralize(false);
                entityType.SetTableName(pluralTableName);
            }
        }
    }
}
