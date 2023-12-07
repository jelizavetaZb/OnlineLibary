using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.IdentityConfiguration
{
    public class IdentityUserTokenConfig<T> : IEntityTypeConfiguration<T> where T : IdentityUserToken<int>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(t => new { t.UserId, t.LoginProvider, t.Name });
            builder.ToPluralEntityTypeName();
        }
    }
}
