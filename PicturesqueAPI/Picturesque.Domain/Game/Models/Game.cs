using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class Game
    {
        public Game(
            string categoryId,
            Difficulty difficulty,
            List<Picture> pictures
            )
        {
            CategoryId = categoryId;
            Difficulty = difficulty;
            NumberOfTiles = GetNumberOfTiles(Difficulty);
            Pictures = GetPictures(pictures);
        }

        public string CategoryId { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public int NumberOfTiles { get; private set; }
        public List<string> Pictures { get; set; }

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

        private List<string> GetPictures(List<Picture> pictures)
        {
            List<string> picturePaths = new List<string>();

            foreach (var picture in pictures)
            {
                picturePaths.Add(picture.Img2Base64);
            }

            return picturePaths;
        }
    }
}
