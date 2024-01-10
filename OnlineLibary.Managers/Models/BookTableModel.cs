namespace OnlineLibary.Managers.Models
{
    public class BookTableModel
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }
        public int ChapterCount { get; set; }
        public DateTime DateUpdated { get; set; }
    }
}
