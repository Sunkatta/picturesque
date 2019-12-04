using System;
using System.Collections.Generic;
using System.Text;

namespace Picturesque.Application
{
    public sealed class LoginUserEntry
    {
        public string Id { get; set; }
        public string Email { get; set; }
        public string Password { get; set; }
        public bool IsAdmin { get; set; }
    }
}
