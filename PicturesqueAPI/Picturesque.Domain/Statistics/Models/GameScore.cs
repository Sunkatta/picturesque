using Picturesque.Common;
using System;
using System.Collections.Generic;
using System.Text;

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
            Difficulty difficulty,
            CustomId id = null
        )
        {
            UserId = userId;
            CategoryId = categoryId;
            Score = score;
            Difficulty = difficulty;
            _id = id ?? new CustomId();
        }

        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public int Score { get; set; }
        public Difficulty Difficulty { get; set; }
    }
}
