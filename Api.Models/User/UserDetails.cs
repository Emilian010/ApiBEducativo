using System.ComponentModel.DataAnnotations;

namespace Api.Models.User
{
    public class UserLogin
    {
        [Required]
        public string Password { get; set; }

        [Required]
        public string Email { get; set; }

        public string ConfirmPassword { get; set; }
    }
}
