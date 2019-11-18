using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class Game
    {
        public Game(
            Category category,
            Difficulty difficulty
            )
        {
            Category = category;
            Difficulty = difficulty;
        }

        public Category Category { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
