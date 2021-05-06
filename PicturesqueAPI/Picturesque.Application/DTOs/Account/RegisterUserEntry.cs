using System.ComponentModel.DataAnnotations;

namespace Picturesque.Application
{
    public sealed class RegisterUserEntry
    {
        [Required]
        public string Username { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        [MinLength(5)]
        public string Password { get; set; }

        public string DefaultProfilePic { get; set; }
    }
}
