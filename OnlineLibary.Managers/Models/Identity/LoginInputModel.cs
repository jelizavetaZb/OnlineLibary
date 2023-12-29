using System.ComponentModel.DataAnnotations;

namespace OnlineLibary.Managers.Models.Identity
{
    public class LoginInputModel
    {  
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }

    }
}
