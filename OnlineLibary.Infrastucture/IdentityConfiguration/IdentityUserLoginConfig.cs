using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Infrastructure.Extensions;

namespace OnlineLibary.Infrastructure.IdentityConfiguration
{
    public class IdentityUserLoginConfig<T> : IEntityTypeConfiguration<T> where T : IdentityUserLogin<int>
    {
        public virtual void Configure(EntityTypeBuilder<T> builder)
        {
            builder.HasKey(l => new { l.LoginProvider, l.ProviderKey });
            builder.ToPluralEntityTypeName();
        }
    }
}
