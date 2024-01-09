using Microsoft.AspNetCore.Http;
using OnlineLibary.Managers.Helpers;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibary.Managers.Models
{
    public class BookEditInputModel
    {
        public int? Id { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Title { get; set; }

        [StringLength(1000, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 20)]
        public string Description { get; set; }

        [Required]
        [YearRange(1000, ErrorMessage = "The {0} must be at least {2} and at max {1}.")]
        public int Year { get; set; }
        public string CoverUrl { get; set; }

        [Required]
        [StringLength(100, ErrorMessage = "The {0} must be at least {2} and at max {1} characters long.", MinimumLength = 2)]
        public string Author { get; set; }

        public int? CurrentChapterId { get; set; }
        public IFormFile NewCover { get; set; }
    }
}
