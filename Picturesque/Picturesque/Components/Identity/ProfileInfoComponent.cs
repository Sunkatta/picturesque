using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class ProfileInfoComponent : ComponentBase
    {
        [Inject]
        protected MatBlazor.IMatToaster Toaster { get; set; }

        protected string token;
        protected string username;
        protected string id;

        protected bool hasEmailBeenSent;
        protected Profile profile;
        protected HttpClient httpClient;

        protected async Task GetProfile()
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            profile = await httpClient.GetJsonAsync<Profile>(ApiConstants.ApiUrl + "Account/Profile/" + id);
            username = profile.Username;
        }

        protected async Task HandleProfileUpdate()
        {
            try
            {
                httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
                var response = await httpClient.PostJsonAsync<Profile>(ApiConstants.ApiUrl + "Account/Profile/" + id, profile);
                profile = response;
                Toaster.Add("Profile updated successfully", MatBlazor.MatToastType.Success);
            }
            catch (Exception)
            {
                Toaster.Add("There was an error while updating your profile", MatBlazor.MatToastType.Danger);
            }
        }

        protected async Task SendForgotPasswordEmail()
        {
            EmailInputModel emailInputModel = new EmailInputModel()
            {
                Email = profile.Email,
            };

            await httpClient.PostJsonAsync(ApiConstants.ApiUrl + "Account/ForgotPassword", emailInputModel);
            hasEmailBeenSent = true;
            StateHasChanged();
        }
    }
}
