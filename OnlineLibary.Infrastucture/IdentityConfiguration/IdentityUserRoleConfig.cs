using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.IdentityConfiguration
{
    public class IdentityUserRoleConfig<T> : IEntityTypeConfiguration<T> where T : IdentityUserRole<int>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(r => new { r.UserId, r.RoleId });
            builder.ToPluralEntityTypeName();
        }
    }
}
