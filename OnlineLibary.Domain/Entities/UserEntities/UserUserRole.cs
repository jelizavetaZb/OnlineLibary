using Microsoft.AspNetCore.Identity;

namespace OnlineLibary.Domain.Entities.UserEntities
{
    public class UserUserRole : IdentityUserRole<int>
    {
        public virtual User User { get; set; }
        public virtual UserRole Role { get; set; }
    }
}
