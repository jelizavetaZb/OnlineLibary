using System.ComponentModel;

namespace OnlineLibary.Domain.Enums
{
    public enum UserRoleType
    {
        [Description("Global Admin")]
        GlobalAdmin = 0, 
        [Description("User")]
        User = 10, 
        [Description("Editor")]
        Editor = 20
    }
}
