using Microsoft.AspNetCore.Http;
using System.ComponentModel.DataAnnotations;

namespace OnlineLibary.Managers.Models
{
    public class ChapterEditInputModel
    {
        public int? Id { get; set; }

        [MaxLength(200, ErrorMessage = "{0} length can't be more than {1}")]
        public string Title { get; set; }

        [Required]
        [MaxLength(2097152, ErrorMessage = "{0} length can't be more than {1}")]
        public string Content { get; set; }

        [Required]
        [Range(0, 10000)]
        public int Number { get; set; }

        public string ImageUrl { get; set; }
        public int BookId { get; set; }
        public int? PreviousId { get; set; }
        public int? NextId { get; set; }

        public IFormFile NewImage { get; set; }
    }
}
