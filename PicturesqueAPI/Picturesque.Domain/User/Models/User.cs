using Microsoft.AspNetCore.Identity;
using System;
using System.Collections.Generic;

namespace Picturesque.Domain
{
    public sealed class User : IdentityUser
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProfilePic { get; set; }

        public ICollection<GameScore> GameScores { get; private set; } =
            new HashSet<GameScore>();

        public ICollection<UserStatistics> UserStatistics { get; private set; } =
            new HashSet<UserStatistics>();
    }
}
