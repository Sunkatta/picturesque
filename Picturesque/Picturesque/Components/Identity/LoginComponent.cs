using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System;
using System.Collections.Generic;
using System.ComponentModel.DataAnnotations;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class LoginComponent : ComponentBase
    {
        public LoginInputModel loginInputModel = new LoginInputModel();

        protected async Task HandleLogin()
        {
            HttpClient client = new HttpClient();
            await client.PostJsonAsync("https://localhost:44317/api/Account/Login", loginInputModel);
        }
    }
}
