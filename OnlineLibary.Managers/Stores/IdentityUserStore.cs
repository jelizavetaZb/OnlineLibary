using Microsoft.AspNetCore.Identity.EntityFrameworkCore;
using OnlineLibary.Domain.Entities.UserEntities;
using OnlineLibary.Infrastructure;

namespace OnlineLibary.Managers.Stores
{
    public class IdentityUserStore : UserStore<User, UserRole, ApplicationDbContext, int, UserClaim, UserUserRole, UserLogin, UserToken, UserRoleClaim>
    {
        public IdentityUserStore(ApplicationDbContext context) : base(context)
        {
        }
    }
}
