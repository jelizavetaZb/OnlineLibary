using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure;

namespace OnlineLibary.Managers.Stores
{
    public class IdentityRoleStore : RoleStore<UserRole, ApplicationDbContext, int, UserUserRole, UserRoleClaim>
    {
        public IdentityRoleStore(ApplicationDbContext context, IdentityErrorDescriber describer = null) : base(context, describer)
        {
        }
    }
}
