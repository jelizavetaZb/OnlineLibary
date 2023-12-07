using Microsoft.AspNetCore.Identity;

namespace OnlineLibary.Domain.Entities.UserEntities;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser<int>
{
    public DateTime DateCreated { get; set; }
    public virtual ICollection<UserUserRole> UserRoles { get; set; }
}

