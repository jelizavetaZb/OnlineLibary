namespace OnlineLibary.Managers.Models.Identity
{
    public class UserRoleEditModel
    {
        public int Id { get; set; }
        public string FirstName { get; set; }
        public string LastName { get; set; }
        public string Email { get; set; }
        public string ProfileImage { get; set; }

        public List<UserRoleModel> Roles { get; set; }
    }
}
