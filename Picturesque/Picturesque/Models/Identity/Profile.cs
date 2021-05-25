using System;

namespace Picturesque.Models
{
    public class Profile
    {
        public string FirstName { get; set; }

        public string LastName { get; set; }

        public string Email { get; set; }

        public string Username { get; set; }

        public DateTime CreatedOn { get; set; }

        public string ProfilePic { get; set; }
    }
}
