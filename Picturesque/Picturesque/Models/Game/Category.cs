using System.ComponentModel.DataAnnotations;

namespace Picturesque.Models
{
    public class Category
    {
        public string Id { get; set; }

        [Required]
        public string Name { get; set; }
    }
}
