using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    [CustomAuthorize(UserRoleType.Editor, UserRoleType.Reader)]
    public class ChapterModel : PageModel
    {
        private readonly ChapterManager _chapterManager;
        private readonly UserCustomManager _userManager;
        private readonly RecordManager _recordManager;

        public ChapterModel(ChapterManager chapterManager, UserCustomManager userManager, RecordManager recordManager)
        {
            _chapterManager = chapterManager;
            _userManager = userManager;
            _recordManager = recordManager;
        }

        public ChapterEditInputModel Display { get; set; }

        public IActionResult OnGet(int bookId, int? id = null)
        {
            if (bookId == 0)
            {
                return Redirect(PagesList.Error);
            }
            Display = _chapterManager.GetChapterEditInputModel(id) ?? new();
            Display.BookId = bookId;
            var userId = _userManager.GetCurrentUserId();
            if (id.HasValue)
            {
                _recordManager.UpdateRecord(bookId, id.Value, userId);
            }
            return Page();
        }
    }
}
