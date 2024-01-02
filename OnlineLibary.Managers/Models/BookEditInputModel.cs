using Microsoft.AspNetCore.Http;

namespace OnlineLibary.Managers.Models
{
    public class BookEditInputModel
    {
        public int? Id { get; set; }
        public string Title { get; set; }
        public string Description { get; set; }
        public int Year { get; set; }
        public string CoverUrl { get; set; }
        public string Author { get; set; }

        public IFormFile NewCover { get; set; }
    }
}
