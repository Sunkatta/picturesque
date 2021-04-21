using Picturesque.Models.Enums;

namespace Picturesque.Models
{
    public class GameScore
    {
        public GameScore() {}

        public GameScore(
            string userId,
            string categoryId,
            int difficulty,
            int score,
            int completedInSeconds,
            int numberOfMistakes,
            bool isHelpUsed)
        {
            UserId = userId;
            CategoryId = categoryId;
            Difficulty = (Difficulty)difficulty;
            Score = score;
            CompletedInSeconds = completedInSeconds;
            NumberOfMistakes = numberOfMistakes;
            IsHelpUsed = isHelpUsed;
        }

        public string UserId { get; set; }

        public string Username { get; set; }
        
        public string CategoryId { get; set; }
        
        public string CategoryName { get; set; }
        
        public Difficulty Difficulty { get; set; }
        
        public int Score { get; set; }

        public int CompletedInSeconds { get; set; }

        public int NumberOfMistakes { get; set; }

        public bool IsHelpUsed { get; set; }
    }
}
