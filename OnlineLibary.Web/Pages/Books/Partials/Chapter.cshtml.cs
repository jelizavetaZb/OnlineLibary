using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    [Authorize]
    public class ChapterModel : PageModel
    {
        private readonly BookManager _bookManager;
        private readonly UserCustomManager _userManager;

        public ChapterModel(BookManager bookManager, UserCustomManager userManager)
        {
            _bookManager = bookManager;
            _userManager = userManager;
        }

        [BindProperty]
        public ChapterEditInputModel Input { get; set; }

        public IActionResult OnGet(int bookId, int? id = null)
        {
            if (bookId == 0)
            {
                return Redirect(PagesList.Error);
            }
            UpdateInput(bookId, id);
            var userId = _userManager.GetCurrentUserId();
            _bookManager.UpdateRecord(bookId, id.Value, userId);
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;
            var bookId = Input.BookId;
            if (!User.HasAnyRole(UserRoleType.Editor))
            {
                return Redirect(PagesList.Error);
            }

            if (ModelState.IsValid)
            {
                var result = await _bookManager.InsertOrUpdateChapterAsync(Input);
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
            UpdateInput(bookId, id);
            return Page();
        }
        public IActionResult OnGetDelete(int bookId, int id)
        {
            if (User.HasAnyRole(UserRoleType.Editor))
            {
                _bookManager.DeleteChapter(id);
            }
            return RedirectToAction(PagesList.BooksDetails, new { id = bookId });
        }

        public void UpdateInput(int bookId, int? id)
        {
            Input = _bookManager.GetChapterEditInputModel(id) ?? new();
            Input.BookId = bookId;
        }
    }
}
