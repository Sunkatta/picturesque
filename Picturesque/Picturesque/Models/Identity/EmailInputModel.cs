using System.ComponentModel.DataAnnotations;

namespace Picturesque.Models
{
    public class EmailInputModel
    {
        [Required]
        [EmailAddress]
        public string Email { get; set; }
    }
}
