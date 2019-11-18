using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class RegisterComponent : ComponentBase
    {
        public RegisterInputModel registerInputModel = new RegisterInputModel();

        protected async Task HandleRegister()
        {
            HttpClient client = new HttpClient();
            await client.PostJsonAsync("https://localhost:44317/api/Account/Register", registerInputModel);
        }

        public class RegisterInputModel
        {
            [Required]
            [EmailAddress]
            public string Email { get; set; }

            [Required]
            public string Username { get; set; }

            [Required]
            [DataType(DataType.Password)]
            public string Password { get; set; }

            [DataType(DataType.Password)]
            [Compare("Password", ErrorMessage = "The password and confirmation password do not match.")]
            public string ConfirmPassword { get; set; }
        }
    }
}
