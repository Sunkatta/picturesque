using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class Game
    {
        public Game(
            string categoryId,
            Difficulty difficulty
            )
        {
            CategoryId = categoryId;
            Difficulty = difficulty;
            NumberOfTiles = GetNumberOfTiles(Difficulty);
        }

        public string CategoryId { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public int NumberOfTiles { get; private set; }

        private int GetNumberOfTiles(Difficulty difficulty)
        {
            int numberOfTiles = 0;

            switch (difficulty)
            {
                case Difficulty.Easy: numberOfTiles = 16;
                    break;
                case Difficulty.Medium: numberOfTiles = 36;
                    break;
                case Difficulty.Hard: numberOfTiles = 64;
                    break;
                default: numberOfTiles = 0;
                    break;
            }
            return numberOfTiles;
        }
    }
}
