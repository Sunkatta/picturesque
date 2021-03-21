using System.ComponentModel.DataAnnotations;

namespace Picturesque.Application
{
    public class ResetPasswordEntry
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }

        [Required]
        [MinLength(5)]
        public string Password { get; set; }

        public string PasswordResetCode { get; set; }
    }
}
