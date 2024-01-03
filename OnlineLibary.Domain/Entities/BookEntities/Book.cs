using OnlineLibary.Domain.Entities.UserEntities;

namespace OnlineLibary.Domain.Entities.BookEntities
{
    public class Book
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }
        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
        public virtual ICollection<Chapter> Chapters { get; set; }
        public virtual ICollection<UserBook> Records { get; set; }
    }
}
