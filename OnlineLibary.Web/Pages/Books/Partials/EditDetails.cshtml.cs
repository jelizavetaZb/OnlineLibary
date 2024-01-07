using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    [CustomAuthorize(UserRoleType.Editor)]
    public class EditDetailsModel : PageModel
    {
        private readonly ChapterManager _chapterManager;
        private readonly UserCustomManager _userManager;
        private readonly BookManager _bookManager;

        public EditDetailsModel(ChapterManager chapterManager, UserCustomManager userManeger, BookManager bookManeger)
        {
            _chapterManager = chapterManager;
            _userManager = userManeger;
            _bookManager = bookManeger;
        }

        [BindProperty]
        public BookEditInputModel Input { get; set; }
        public List<ChapterTableModel> Chapters { get; set; }

        public void OnGet(int? id = null)
        {
            UpdateInput(id);
            Chapters = _chapterManager.GetChapterTable(id) ?? new List<ChapterTableModel>();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;
            if (ModelState.IsValid)
            {
                var result = await _bookManager.InsertOrUpdateBookAsync(Input);
                if (result.IsSuccess)
                {
                    id = result.UpdatedId;
                    return RedirectToPage(PagesList.BooksDetails, new { id });
                }
                if (result.Errors.Any())
                {
                    foreach (var (key, error) in result.Errors)
                    {
                        ModelState.AddModelError(key, error);
                    }
                }

                UpdateInput(id);
            }
            Chapters = _chapterManager.GetChapterTable(id) ?? new List<ChapterTableModel>();
            return Page();
        }

        private void UpdateInput(int? id)
        {
            var userId = User.Identity.IsAuthenticated ? (int?)_userManager.GetCurrentUserId() : null;
            Input = _bookManager.GetBookEditInputModel(id, userId) ?? new BookEditInputModel();
        }
    }
}
