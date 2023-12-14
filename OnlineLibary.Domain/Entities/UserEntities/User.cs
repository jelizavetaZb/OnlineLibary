using Microsoft.AspNetCore.Identity;

namespace OnlineLibary.Domain.Entities.UserEntities;

// Add profile data for application users by adding properties to the User class
public class User : IdentityUser<int>
{
    public DateTime DateCreated { get; set; }
    public string ProfileImage { get; set; }
    public string FirstName { get; set; }
    public string LastName { get; set; }
    public virtual ICollection<UserUserRole> UserRoles { get; set; }
}

