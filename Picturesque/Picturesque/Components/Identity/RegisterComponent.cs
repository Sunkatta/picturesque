using Microsoft.AspNetCore.Components;
using Picturesque.Models;
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
    }
}
