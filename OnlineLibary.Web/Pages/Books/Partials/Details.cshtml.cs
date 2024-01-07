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
        private readonly ChapterManager _chapterManager;
        private readonly UserCustomManager _userManager;
        private readonly BookManager _bookManager;

        public EditBookModel(ChapterManager chapterManager, UserCustomManager userManeger, BookManager bookManeger)
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
            var userId = User.Identity.IsAuthenticated ? (int?)_userManager.GetCurrentUserId() : null;
            Input = _bookManager.GetBookEditInputModel(id, userId) ?? new BookEditInputModel();
            Chapters = _chapterManager.GetChapterTable(id) ?? new List<ChapterTableModel>();
        }
    }
}
