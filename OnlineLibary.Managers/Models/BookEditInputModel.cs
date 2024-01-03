using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibary.Managers.Models
{
    public class BookEditInputModel
    {
        public int? Id { get; set; }

        [Required]
        public string Title { get; set; }
        public string Description { get; set; }

        [Required]
        public int Year { get; set; }
        public string CoverUrl { get; set; }

        [Required]
        public string Author { get; set; }

        public int? CurrentChapterId { get; set; }
        public IFormFile NewCover { get; set; }
    }
}
