using Microsoft.AspNetCore.Components;
using Newtonsoft.Json;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class RegisterComponent : ComponentBase
    {
        public RegisterInputModel registerInputModel = new RegisterInputModel();

        public bool hasSuccessfullyRegistered;
        public bool loading;
        public string errorMessage;

        protected async Task HandleRegister()
        {
            loading = true;
            errorMessage = string.Empty;
            StateHasChanged();
            registerInputModel.DefaultProfilePic = ApiConstants.DefaultProfilePicInBase64;
            HttpClient client = new HttpClient();
            var response = await client.PostAsync(
                ApiConstants.ApiUrl + "Account/Register",
                new StringContent(JsonConvert.SerializeObject(registerInputModel), Encoding.UTF8, "application/json"));

            if (response.StatusCode == System.Net.HttpStatusCode.BadRequest)
            {
                errorMessage = await response.Content.ReadAsStringAsync();
                errorMessage = errorMessage.Trim('"');
                return;
            }

            hasSuccessfullyRegistered = true;
            StateHasChanged();
            loading = false;
        }
    }
}
