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
        public List<BookTableModel> Table { get; set; }

        public ListModel(BookManager bookManager)
        {
            _bookManager = bookManager;
        }

        public void OnGet()
        {
            Table = _bookManager.GetBookGridModels();
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
