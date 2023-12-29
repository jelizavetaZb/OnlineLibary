using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Managers.Managers;

namespace OnlineLibary.Web.Pages.Account
{
    public class LogoutModel : PageModel
    {
        private readonly UserCustomManager _authManager;
        public LogoutModel(UserCustomManager authManager)
        {
            _authManager = authManager;
        }

        public async Task<IActionResult> OnGet()
        {
            await _authManager.SingOut();
            return LocalRedirect(PagesList.Index);
        }

        public async Task<IActionResult> OnPost()
        {
            await _authManager.SingOut();
            return LocalRedirect(PagesList.Index);
        }
    }
}
