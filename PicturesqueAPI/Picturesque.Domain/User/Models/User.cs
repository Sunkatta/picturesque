using Microsoft.AspNetCore.Identity;
using System;

namespace Picturesque.Domain
{
    public sealed class User : IdentityUser
    {
        public bool IsAdmin { get; set; }

        public bool IsBlocked { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProfilePic { get; set; }
    }
}
