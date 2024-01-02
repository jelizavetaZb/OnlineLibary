using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    public class EditBookModel : PageModel
    {
        private readonly BookManager _bookManager;

        public EditBookModel(BookManager bookManager)
        {
            _bookManager = bookManager;
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
            if (!User.HasAnyRole(UserRoleType.Editor))
            {
                return Redirect(PagesList.Error);
            }

            if (ModelState.IsValid)
            {
                var result = await _bookManager.InsertOrUpdateBookAsync(Input);
                if (result.IsSuccess)
                {
                    UpdateInput(result.UpdatedId);
                }
                if (result.Errors.Any())
                {
                    foreach (var (key, error) in result.Errors)
                    {
                        ModelState.AddModelError(key, error);
                    }
                }
            }
            return Page();
        }

        private void UpdateInput(int? id)
        {
            Input = _bookManager.GetBookEditInputModel(id) ?? new BookEditInputModel();
            Chapters = _bookManager.GetChapterTable(id) ?? new List<ChapterTableModel>();
        }
    }
}
