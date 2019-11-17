using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Text;

namespace Picturesque.Application
{
    public sealed class RegisterUserEntry
    {
        [Required]
        public string Username { get; set; }


        [EmailAddress]
        public string Email { get; set; }

        [MinLength(5)]
        public string Password { get; set; }
    }
}
