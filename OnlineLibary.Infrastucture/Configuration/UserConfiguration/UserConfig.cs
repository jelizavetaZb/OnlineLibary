using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.Extensions;
using OnlineLibary.Infrastructure.IdentityConfiguration;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserConfig : IdentityUserConfig<User, UserLogin, UserToken, UserUserRole>
    {
        public override void Configure(EntityTypeBuilder<User> builder)
        {
            base.Configure(builder);
            builder.Property(e => e.DateCreated).DatabaseGenerated();
        }
    }
}
