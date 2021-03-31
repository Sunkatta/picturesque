using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class ResendEmailComponent : ComponentBase
    {
        public EmailInputModel emailInputModel = new EmailInputModel();
        public bool hasEmailBeenSent = false;

        public async Task HandleResendEmail()
        {
            HttpClient client = new HttpClient();
            await client.PostJsonAsync(ApiConstants.ApiUrl + "Account/ResendConfirmationEmail", emailInputModel);
            hasEmailBeenSent = true;
        }
    }
}
