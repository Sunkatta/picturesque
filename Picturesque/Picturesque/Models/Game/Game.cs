using Newtonsoft.Json;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Models
{
    public class Game
    {
        public string CategoryId { get; set; }
        public int Difficulty { get; set; }
        public int NumberOfTiles { get; set; }
        public List<string> Pictures { get; set; }
    }
}
