using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models.Identity;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Account
{
    [CustomAuthorize(UserRoleType.UserManager)]
    public class UserManagementModel : PageModel
    {
        private readonly UserCustomManager _userManager;
        public UserManagementModel(UserCustomManager userCustomManager)
        {
            _userManager = userCustomManager;
        }

        public List<UserTableModel> Table {  get; set; }

        public void OnGet()
        {
            Table = _userManager.GetUsersTable();
        }
    }
}
