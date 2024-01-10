using Humanizer;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;


namespace OnlineLibary.Infrastructure.Extensions
{
    public static class EntityTypeBuilderExtensions
    {

        public static EntityTypeBuilder<T> ToPluralEntityTypeName<T>(this EntityTypeBuilder<T> builder)
            where T : class => builder.ToTable(typeof(T).Name.Pluralize(false));


        public static PropertyBuilder<DateTime> DatabaseGenerated(this PropertyBuilder<DateTime> builder)
        {
            var sql = ProviderName switch
            {
                SqlServer => "getutcdate()",
                Sqlite => "STRFTIME('%Y-%m-%d %H:%M:%f', 'NOW')",
                _ => throw new NotImplementedException($"Unsupported Database provider! Provider: {ProviderName}")
            };
            return builder.HasDefaultValueSql(sql).ValueGeneratedOnAdd();
        }

        public static PropertyBuilder<DateTime?> DatabaseGenerated(this PropertyBuilder<DateTime?> builder)
        {
            var sql = ProviderName switch
            {
                SqlServer => "getutcdate()",
                Sqlite => "STRFTIME('%Y-%m-%d %H:%M:%f', 'NOW')",
                _ => throw new NotImplementedException($"Unsupported Database provider! Provider: {ProviderName}")
            };
            return builder.HasDefaultValueSql(sql).ValueGeneratedOnAdd();
        }

        public static string ProviderName { private get; set; } = SqlServer;

        private const string
            SqlServer = "Microsoft.EntityFrameworkCore.SqlServer",
            Sqlite = "Microsoft.EntityFrameworkCore.Sqlite";

    }
}
