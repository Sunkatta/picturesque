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
            Pictures = pictures;
        }

        public string CategoryId { get; private set; }
        public Difficulty Difficulty { get; private set; }
        public List<Picture> Pictures { get; set; }

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
