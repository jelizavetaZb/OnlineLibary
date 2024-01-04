using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.RazorPages;

namespace OnlineLibary.Web.Pages
{
    public class IndexModel : PageModel
    {

        public IndexModel(ILogger<IndexModel> logger)
        {
            _logger = logger;
        }

        public IActionResult OnGet()
        {
            return Redirect(PagesList.BooksList);
        }
    }
}
