using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.IdentityConfiguration;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserRoleConfig : IdentityRoleConfig<UserRole, UserUserRole>
    {
        public override void Configure(EntityTypeBuilder<UserRole> builder)
        {
            base.Configure(builder);
        }
    }
}
