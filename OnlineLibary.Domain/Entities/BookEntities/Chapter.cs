namespace OnlineLibary.Domain.Entities.BookEntities
{
    public class Chapter
    {
        public int Id { get; set; }
        public string Title { get; set; }
        public string Content { get; set; }
        public int Number {  get; set; }
        public string ImageUrl { get; set; }
        public int BookId { get; set; }
        public virtual Book Book { get; set; }

        public DateTime DateCreated { get; set; }
        public DateTime? DateUpdated { get; set; }
    }
}
