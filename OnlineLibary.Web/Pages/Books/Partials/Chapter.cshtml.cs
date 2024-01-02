using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;
using OnlineLibary.Managers.Managers;

namespace OnlineLibary.Web.Pages.Books.Partials
{
    public class ChapterModel : PageModel
    {
        private readonly BookManager _bookManager;

        public ChapterModel(BookManager bookManager)
        {
            _bookManager = bookManager;
        }
        public void OnGet()
        {
        }
    }
}
