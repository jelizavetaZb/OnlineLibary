using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.IdentityConfiguration;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserUserRoleConfig : IdentityUserRoleConfig<UserUserRole>
    {
        public override void Configure(EntityTypeBuilder<UserUserRole> builder)
        {
            base.Configure(builder);
            builder.HasOne(x => x.User).WithMany(x => x.UserRoles).HasForeignKey(x => x.UserId);
            builder.HasOne(x => x.Role).WithMany(x => x.UserRoles).HasForeignKey(x => x.RoleId);
        }
    }
}
