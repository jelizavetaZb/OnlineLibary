using System.ComponentModel;

namespace OnlineLibary.Domain.Enums
{
    public enum UserRoleType
    {
        [Description("User Manager")]
        UserManager = 0, 
        [Description("Reader")]
        Reader = 10, 
        [Description("Editor")]
        Editor = 20
    }
}
