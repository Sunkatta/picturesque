using System;

namespace Picturesque.Domain
{
    public sealed class GameScoreView
    {
        public GameScoreView(
            string username,
            string profilePicture,
            Difficulty difficulty,
            string categoryName,
            int score,
            int numberOfMistakes,
            int completedInSeconds,
            bool isHelpUsed,
            DateTime createdOn)
        {
            Username = username;
            ProfilePicture = profilePicture;
            Difficulty = difficulty;
            CategoryName = categoryName;
            Score = score;
            NumberOfMistakes = numberOfMistakes;
            CompletedInSeconds = completedInSeconds;
            IsHelpUsed = isHelpUsed;
            CreatedOn = createdOn;
        }

        public string Username { get; set; }

        public string ProfilePicture { get; set; }

        public Difficulty Difficulty { get; set; }
        
        public string CategoryName { get; set; }
        
        public int Score { get; set; }

        public int NumberOfMistakes { get; set; }

        public int CompletedInSeconds { get; set; }

        public bool IsHelpUsed { get; set; }

        public DateTime CreatedOn { get; set; }
    }
}
