namespace OnlineLibary.Web.Pages.Models
{
    public class ChapterNavigationModel
    {
        public int BookId { get; set; }
        public int? PreviousId { get; set; }
        public int? NextId { get; set;}

        public ChapterNavigationModel(int bookId, int? previousId, int? nextId)
        {
            BookId = bookId;
            PreviousId = previousId;
            NextId = nextId;
        }
    }
}
