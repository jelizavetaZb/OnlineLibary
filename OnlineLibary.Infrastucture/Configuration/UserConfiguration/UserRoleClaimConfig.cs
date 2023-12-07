using Microsoft.EntityFrameworkCore.Metadata.Builders;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure.IdentityConfiguration;

namespace OnlineLibary.Infrastructure.Configuration.UserConfiguration
{
    public class UserRoleClaimConfig : IdentityRoleClaimConfig<UserRoleClaim> { }
}
