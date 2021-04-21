namespace Picturesque.Domain
{
    public sealed class GameScoreView
    {
        public GameScoreView(
            string username,
            Difficulty difficulty,
            string categoryName,
            int score,
            int numberOfMistakes,
            int completedInSeconds,
            bool isHelpUsed)
        {
            Username = username;
            Difficulty = difficulty;
            CategoryName = categoryName;
            Score = score;
            NumberOfMistakes = numberOfMistakes;
            CompletedInSeconds = completedInSeconds;
            IsHelpUsed = isHelpUsed;
        }

        public string Username { get; set; }

        public Difficulty Difficulty { get; set; }
        
        public string CategoryName { get; set; }
        
        public int Score { get; set; }

        public int NumberOfMistakes { get; set; }

        public int CompletedInSeconds { get; set; }

        public bool IsHelpUsed { get; set; }
    }
}
