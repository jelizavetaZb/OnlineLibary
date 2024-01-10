using OnlineLibary.Managers.Models;

namespace OnlineLibary.Web.Pages.Models
{
    public class ChapterListModel
    {
        public int? BookId { get; set; }
        public List<ChapterTableModel> Chapters { get; set; }

        public ChapterListModel(int? bookId, List<ChapterTableModel> chapters)
        {
            BookId = bookId;
            Chapters = chapters;
        }
    }
}
