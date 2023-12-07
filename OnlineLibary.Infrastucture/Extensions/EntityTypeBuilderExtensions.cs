using System.Diagnostics.CodeAnalysis;
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
        public static PropertyBuilder<Guid> DatabaseGenerated(this PropertyBuilder<Guid> builder)
        {
            var sql = ProviderName switch
            {
                SqlServer => "newid()",
                // Sqlite: There is no native method for guid generation, however trigger can be created for that, see https://www.kittell.net/code/auto-increment-auto-generate-guid/
                _ => throw new NotImplementedException($"Unsupported Database provider! Provider: {ProviderName}")
            };
            return builder.HasDefaultValueSql(sql).ValueGeneratedOnAdd();
        }

        public static PropertyBuilder<bool> DatabaseGenerated(this PropertyBuilder<bool> builder)
        {
            var sql = ProviderName switch
            {
                SqlServer => "((0))",
                // Sqlite: 
                _ => throw new NotImplementedException($"Unsupported Database provider! Provider: {ProviderName}")
            };
            return builder.HasDefaultValueSql(sql);
        }

        public static PropertyBuilder<decimal?> Precision(this PropertyBuilder<decimal?> builder, int totalNumbers,
            int decimalNumbers) => builder.HasColumnType($"decimal({totalNumbers}, {decimalNumbers})");

        public static PropertyBuilder<decimal> Precision(this PropertyBuilder<decimal> builder, int totalNumbers,
            int decimalNumbers) => builder.HasColumnType($"decimal({totalNumbers}, {decimalNumbers})");

        #region Helpers

        public static string ProviderName { private get; set; } = SqlServer;

        private const string
            SqlServer = "Microsoft.EntityFrameworkCore.SqlServer",
            Sqlite = "Microsoft.EntityFrameworkCore.Sqlite";

        #endregion
    }
}
