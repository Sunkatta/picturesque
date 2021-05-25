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
        protected string token;
        protected string username;
        protected string errorMessage;
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
            }
            catch (Exception)
            {
                errorMessage = "There was an error while updating your profile...";
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
