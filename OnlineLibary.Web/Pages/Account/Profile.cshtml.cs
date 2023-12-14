using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models.Identity;

namespace OnlineLibary.Web.Pages.Account
{
    public class ProfileModel : PageModel
    {
        private readonly AuthManager _authManager;

        public ProfileModel(AuthManager authManager)
        {
            _authManager = authManager;
        }

        public ProfileEditModel Input {  get; set; }

        public void OnGet(int? id)
        {
            Input = _authManager.GetUserProfile(id);
        }
    }
}
