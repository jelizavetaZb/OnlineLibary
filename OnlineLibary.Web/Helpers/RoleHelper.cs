using OnlineLibary.Domain.Enums;
using System.Security.Principal;

namespace OnlineLibary.Web.Helpers
{
    public static class RoleHelper
    {
        public static bool HasAnyRole(this IPrincipal user, params UserRoleType[] roles)
        {
            var allowedRoles = roles.Select(x => x.ToString());
            return allowedRoles.Any(user.IsInRole);
        }
    }
}
