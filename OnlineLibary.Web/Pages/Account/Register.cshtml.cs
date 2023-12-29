using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models.Identity;

namespace OnlineLibary.Web.Pages.Account
{
    [AllowAnonymous]
    public class RegisterModel : PageModel
    {
        private readonly UserCustomManager _authManager;

        public RegisterModel(UserCustomManager authManager)
        {
            _authManager = authManager;
        }

        [BindProperty]
        public RegisterInputModel Input { get; set; }
        public string ReturnUrl { get; set; }


        public void OnGetAsync(string returnUrl = null)
        {
            ReturnUrl = returnUrl;
        }

        public async Task<IActionResult> OnPostAsync(string returnUrl = null)
        {
            returnUrl ??= Url.Content("~/");
            if (ModelState.IsValid)
            {
                var result = await _authManager.RegisterUser(Input);

                if (result.Succeeded)
                {
                    return LocalRedirect(returnUrl);
                }

                foreach (var error in result.Errors)
                {
                    ModelState.AddModelError(string.Empty, error.Description);
                }
            }
            return Page();
        }
    }
}
