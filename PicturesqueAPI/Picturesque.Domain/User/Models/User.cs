using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class User
    {
        public User() { }

        public User(
            string email,
            string username,
            string password
            )
        {
            Email = email;
            Username = username;
            Password = password;
            IsAdmin = false;
            IsBlocked = false;
        }

        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}
