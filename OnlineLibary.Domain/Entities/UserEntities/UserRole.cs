using Microsoft.AspNetCore.Identity;
using OnlineLibary.Domain.Enums;

namespace OnlineLibary.Domain.Entities.UserEntities
{
    public class UserRole : IdentityRole<int>
    {
        public UserRoleType UserRoleType { get; set; }
        public string Description { get; set; }
        public virtual ICollection<UserUserRole> UserRoles { get; set; }
    }
}
