using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class ProfileInfoComponent : ComponentBase
    {
        protected string token;
        protected Profile profile;
        protected HttpClient httpClient;

        protected async Task GetProfile(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            profile = await httpClient.GetJsonAsync<Profile>(ApiConstants.ApiUrl + "Account/Profile/" + id);
        }
    }
}
