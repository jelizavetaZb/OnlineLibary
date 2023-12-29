using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models.Identity;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Account
{
    [Authorize]
    public class ProfileModel : PageModel
    {
        private readonly UserCustomManager _userManager;

        public ProfileModel(UserCustomManager authManager)
        {
            _userManager = authManager;
        }

        [BindProperty]
        public ProfileEditModel Input { get; set; }
        public bool IsCurrentUser { get; set; }

        public IActionResult OnGet(int? id)
        {
            UpdateInput(id);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;

            if (id != _userManager.GetCurrentUserId() && !User.HasAnyRole(UserRoleType.GlobalAdmin))
            {
                return Redirect(PagesList.Error);
            }

            if (ModelState.IsValid)
            {
                var result = await _userManager.UpdateUserProfileAsync(Input);
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
            Input = _userManager.GetProfileEditModel(id);
            IsCurrentUser = id == _userManager.GetCurrentUserId();
        }
    }
}
