namespace Picturesque.Application
{
    public sealed class CreateGameScoreEntry
    {
        public string UserId { get; set; }

        public string CategoryId { get; set; }

        public int Difficulty { get; set; }

        public int Score { get; set; }

        public int CompletedInSeconds { get; set; }

        public bool IsHelpUsed { get; set; }

        public int NumberOfMistakes { get; set; }
    }
}
