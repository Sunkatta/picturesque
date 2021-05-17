using Picturesque.Common;
using System;

namespace Picturesque.Domain
{
    public class UserStatistics
    {
        private CustomId _id;

        public UserStatistics() { }

        public UserStatistics(
            string userId,
            int gamesWon,
            int gamesLost,
            int totalScore,
            int totalNumberOfMistakes,
            int totalPlaytime,
            CustomId id = null)
        {
            UserId = userId;
            GamesWon = gamesWon;
            GamesLost = gamesLost;
            TotalScore = totalScore;
            TotalNumberOfMistakes = totalNumberOfMistakes;
            TotalPlaytime = totalPlaytime;
            _id = id ?? new CustomId();
        }

        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public string UserId { get; private set; }

        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int TotalScore { get; set; }

        public int TotalNumberOfMistakes { get; set; }

        public int TotalPlaytime { get; set; }
    }
}
