using Microsoft.AspNetCore.Components;
using Picturesque.Models;
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
            await client.PostJsonAsync("https://localhost:44317/api/Account/ResendConfirmationEmail", emailInputModel);
            hasEmailBeenSent = true;
        }
    }
}
