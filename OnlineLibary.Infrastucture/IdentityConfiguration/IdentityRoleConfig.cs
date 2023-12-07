using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.IdentityConfiguration
{
    public class IdentityRoleConfig<TRole, TUserRole> : IEntityTypeConfiguration<TRole>
            where TRole : IdentityRole<int>
            where TUserRole : IdentityUserRole<int>
    {
        public virtual void Configure(EntityTypeBuilder<TRole> builder)
        {
            builder.HasIndex(r => r.NormalizedName).HasDatabaseName("RoleNameIndex").IsUnique();
            builder.Property(r => r.ConcurrencyStamp).IsConcurrencyToken();

            builder.Property(u => u.Name).HasMaxLength(256);
            builder.Property(u => u.NormalizedName).HasMaxLength(256);

            builder.HasMany<TUserRole>().WithOne().HasForeignKey(ur => ur.RoleId).IsRequired();
            builder.ToPluralEntityTypeName();
        }
    }
}
