using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Picturesque.Models;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class RegisterComponent : ComponentBase
    {
        public RegisterInputModel registerInputModel = new RegisterInputModel();

        public bool hasSuccessfullyRegistered = false;
        public string errorMessage;

        protected async Task HandleRegister()
        {
            errorMessage = string.Empty;
            StateHasChanged();
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(
                "https://localhost:44317/api/Account/Register",
                new StringContent(JsonConvert.SerializeObject(registerInputModel), Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                errorMessage = await response.Content.ReadAsStringAsync();
                errorMessage = errorMessage.Trim('"');
                return;
            }

            hasSuccessfullyRegistered = true;
            StateHasChanged();
        }
    }
}
