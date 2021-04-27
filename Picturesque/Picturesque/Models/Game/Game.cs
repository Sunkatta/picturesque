using System.Collections.Generic;

namespace Picturesque.Models
{
    public class Game
    {
        public string CategoryId { get; set; }
        public int Difficulty { get; set; }
        public int NumberOfTiles { get; set; }
        public List<Picture> Pictures { get; set; }
    }
}
