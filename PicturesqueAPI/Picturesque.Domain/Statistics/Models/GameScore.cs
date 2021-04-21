using Picturesque.Common;
using System;

namespace Picturesque.Domain
{
    public sealed class GameScore
    {
        private CustomId _id;

        public GameScore() { }

        public GameScore(
            string userId,
            string categoryId,
            int score,
            int completedInSeconds,
            int numberOfMistakes,
            Difficulty difficulty,
            bool isHelpUsed,
            CustomId id = null)
        {
            UserId = userId;
            CategoryId = categoryId;
            Score = score;
            CompletedInSeconds = completedInSeconds;
            Difficulty = difficulty;
            IsHelpUsed = isHelpUsed;
            NumberOfMistakes = numberOfMistakes;
            CreatedOn = DateTime.UtcNow;
            _id = id ?? new CustomId();
        }

        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public string UserId { get; private set; }

        public string CategoryId { get; private set; }

        public int Score { get; private set; }

        public int CompletedInSeconds { get; private set; }

        public Difficulty Difficulty { get; private set; }

        public bool IsHelpUsed { get; private set; }

        public int NumberOfMistakes { get; private set; }

        public DateTime CreatedOn { get; private set; }
    }
}
