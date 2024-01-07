using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    [CustomAuthorize(UserRoleType.Editor)]
    public class EditChapterModel : PageModel
    {
        private readonly ChapterManager _chapterManager;

        public EditChapterModel(ChapterManager chapterManager)
        {
            _chapterManager = chapterManager;
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
            return Page();
        }

        public async Task<IActionResult> OnPostAsync()
        {
            var id = Input.Id;
            var bookId = Input.BookId;
            if (ModelState.IsValid)
            {
                var result = await _chapterManager.InsertOrUpdateChapterAsync(Input);
                if (result.IsSuccess)
                {
                    id = result.UpdatedId;
                    return RedirectToPage(PagesList.BooksChapter, new { bookId, id });
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
            _chapterManager.DeleteChapter(id);
            return RedirectToPage(PagesList.BooksDetails, new { id = bookId });
        }

        public void UpdateInput(int bookId, int? id)
        {
            Input = _chapterManager.GetChapterEditInputModel(id) ?? new();
            Input.BookId = bookId;
        }
    }
}
