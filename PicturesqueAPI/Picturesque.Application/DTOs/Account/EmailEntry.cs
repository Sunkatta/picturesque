using System.ComponentModel.DataAnnotations;

namespace Picturesque.Application
{
    public class EmailEntry
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
