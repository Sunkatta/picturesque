using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
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
            await client.PostJsonAsync(ApiConstants.ApiUrl + "Account/ForgotPassword", emailInputModel);
            hasEmailBeenSent = true;
        }
    }
}
