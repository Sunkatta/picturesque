using Picturesque.Common;
using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Domain
{
    public sealed class User
    {
        private CustomId _id;

        public User() { }

        public User(
            string email,
            string username,
            string password,
            bool isAdmin,
            CustomId id = null
            )
        {
            Email = email;
            Username = username;
            Password = HashUtils.CreateHashCode(password);
            IsAdmin = isAdmin;
            IsBlocked = false;
            _id = id ?? new CustomId();
        }

        public string Id
        {
            get { return this._id.ToString(); }
            private set { this._id = new CustomId(new Guid(value)); }
        }

        public string Email { get; private set; }
        public string Username { get; private set; }
        public string Password { get; private set; }
        public bool IsAdmin { get; set; }
        public bool IsBlocked { get; set; }
    }
}
