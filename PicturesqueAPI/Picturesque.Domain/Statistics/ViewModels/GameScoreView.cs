﻿namespace Picturesque.Domain
{
    public sealed class GameScoreView
    {
        public GameScoreView(
            string username,
            Difficulty difficulty,
            string categoryName,
            int score
        )
        {
            Username = username;
            Difficulty = difficulty;
            CategoryName = categoryName;
            Score = score;
        }

        public string Username { get; set; }

        public Difficulty Difficulty { get; set; }
        
        public string CategoryName { get; set; }
        
        public int Score { get; set; }
    }
}