﻿using System.Collections.Generic;

namespace Picturesque.Models
{
    public class UserStatistics
    {
        public int GamesWon { get; set; }

        public int GamesLost { get; set; }

        public int TotalScore { get; set; }

        public int TotalNumberOfMistakes { get; set; }

        public int TotalPlaytime { get; set; }

        public List<int> DailyWonGamesScore { get; set; }
    }
}
