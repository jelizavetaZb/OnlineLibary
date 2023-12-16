using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.ModelBinding;
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

        [BindProperty]
        public ProfileEditModel Input {  get; set; }

        public void OnGet(int? id)
        {
            UpdateInput(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;
            if (ModelState.IsValid)
            {
                var result = await _authManager.UpdateUserProfileAsync(Input);
                id = result.UpdatedId;
                if (result.Errors.Any())
                {
                    foreach (var (key, error) in result.Errors)
                    {
                        ModelState.AddModelError(key, error);
                    }
                }
            }
            UpdateInput(id);
            return Page();
        }

        private void UpdateInput(int? id)
        {
            Input = _authManager.GetUserProfile(id);
        }
    }
}
