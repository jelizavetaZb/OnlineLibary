using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Domain.Enums;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;
using OnlineLibary.Web.Helpers;

namespace OnlineLibary.Web.Pages.Books
{
    [AllowAnonymous]
    public class ListModel : PageModel
    {
        private readonly BookManager _bookManager;
        private readonly UserCustomManager _userManager;

        public ListModel(BookManager bookManager, UserCustomManager userManager)
        {
            _bookManager = bookManager;
            _userManager = userManager;
        }
        public List<BookTableModel> Table { get; set; }

        public void OnGet(bool userBooks = false)
        {
            var id = (userBooks && User.HasAnyRole(UserRoleType.Reader)) 
                ? (int?)_userManager.GetCurrentUserId() 
                : null;
            Table = _bookManager.GetBookGridModels(id);
        }

        public IActionResult OnGetDelete(int id)
        {
            if (User.HasAnyRole(UserRoleType.Editor))
            {
                _bookManager.DeleteBook(id);
            }
            return Redirect(PagesList.BooksList);
        }
    }
}
