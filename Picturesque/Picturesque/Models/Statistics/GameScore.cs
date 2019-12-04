using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace Picturesque.Models
{
    public class GameScore
    {
        public GameScore() { }

        public GameScore(
            string userId,
            string categoryId,
            int diffulty,
            int score)
        {
            UserId = userId;
            CategoryId = categoryId;
            Difficulty = diffulty;
            Score = score;
        }

        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public int Difficulty { get; set; }
        public int Score { get; set; }
    }
}
