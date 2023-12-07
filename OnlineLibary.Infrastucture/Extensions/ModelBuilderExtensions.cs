using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata;

namespace OnlineLibary.Infrastructure.Extensions
{
    public static class ModelBuilderExtensions
    {
        public static void PluralizeTableNames(this ModelBuilder builder)
        {
            foreach (var entityType in builder.Model.GetEntityTypes())
            {
                // Ignore if ToTable (TableName annotation) is explicitly set
                if (entityType.FindAnnotation(RelationalAnnotationNames.TableName) != null) continue;

                var pluralTableName = entityType.DisplayName().Pluralize(false);
                entityType.SetTableName(pluralTableName);
            }
        }

        public static void DisableCascadeDelete(this ModelBuilder builder)
        {
            var relationships = builder.Model.GetEntityTypes()
                .SelectMany(t => t.GetForeignKeys())
                .Where(fk => !fk.IsOwnership && fk.DeleteBehavior == DeleteBehavior.Cascade);
            foreach (var relationship in relationships) relationship.DeleteBehavior = DeleteBehavior.Restrict;
        }
    }
}
