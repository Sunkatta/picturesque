using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class UserComponent : ComponentBase
    {
        [Parameter]
        public string Email { get; set; }

        [Parameter]
        public string ConfirmationCode { get; set; }

        public int counter = 1;
        public string token;

        public List<User> users;

        protected HttpClient client;

        public async Task GetUsers()
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            var response = await client.GetJsonAsync<User[]>(ApiConstants.ApiUrl + "User");
            users = response.ToList();
        }

        public async Task BlockUser(string id)
        {
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            bool isBlocked = await client.PostJsonAsync<bool>(ApiConstants.ApiUrl + "User/Block", id);
            User user = users.FirstOrDefault(u => u.Id == id);
            user.IsBlocked = isBlocked;
            StateHasChanged();
        }

        public async Task<bool> ConfirmEmail()
        {
            return await client.GetJsonAsync<bool>($"{ApiConstants.ApiUrl}Account/ConfirmEmail?email={Email}&emailConfirmationKey={ConfirmationCode}");
        }
    }
}
