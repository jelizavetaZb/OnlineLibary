using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Managers.Managers;
using OnlineLibary.Managers.Models;

namespace OnlineLibary.Web.Pages.Books
{
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
    }
}
