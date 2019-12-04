using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Application
{
    public sealed class CreateGameScoreEntry
    {
        public string UserId { get; set; }
        public string CategoryId { get; set; }
        public int Difficulty { get; set; }
        public int Score { get; set; }
    }
}
