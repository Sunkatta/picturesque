using System.ComponentModel.DataAnnotations;

namespace Picturesque.Models
{
    public class GameOptionsInputModel
    {
        [Required]
        public string Category { get; set; }

        [Required]
        public string Difficulty { get; set; }
    }
}
