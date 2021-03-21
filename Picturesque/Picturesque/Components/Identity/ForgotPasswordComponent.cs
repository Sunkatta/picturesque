using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class ForgotPasswordComponent : ComponentBase
    {
        public EmailInputModel emailInputModel = new EmailInputModel();
        public bool hasEmailBeenSent = false;

        public async Task HandleForgotPassword()
        {
            HttpClient client = new HttpClient();
            await client.PostJsonAsync("https://localhost:44317/api/Account/ForgotPassword", emailInputModel);
            hasEmailBeenSent = true;
        }
    }
}
