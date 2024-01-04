using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    [AllowAnonymous]
    public class EditBookModel : PageModel
    {
        private readonly BookManager _bookManager;
        private readonly UserCustomManager _userManeger;

        public EditBookModel(BookManager bookManager, UserCustomManager userManeger)
        {
            _bookManager = bookManager;
            _userManeger = userManeger;
        }

        [BindProperty]
        public BookEditInputModel Input { get; set; }
        public List<ChapterTableModel> Chapters { get; set; }

        public void OnGet(int? id = null)
        {
            UpdateInput(id);
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;
            if (!User.HasAnyRole(UserRoleType.Editor))
            {
                return Redirect(PagesList.Error);
            }

            if (ModelState.IsValid)
            {
                var result = await _bookManager.InsertOrUpdateBookAsync(Input);
                if (result.IsSuccess)
                {
                    id = result.UpdatedId;
                }
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
            var userId = User.Identity.IsAuthenticated ? (int?)_userManeger.GetCurrentUserId() : null;
            Input = _bookManager.GetBookEditInputModel(id, userId) ?? new BookEditInputModel();
            Chapters = _bookManager.GetChapterTable(id) ?? new List<ChapterTableModel>();
        }
    }
}
