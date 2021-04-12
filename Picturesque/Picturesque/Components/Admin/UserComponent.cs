using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;

namespace Picturesque.Components
{
    public class UserComponent : ComponentBase
    {
        public int counter = 1;
        public string token;

        public List<User> users;

        public async void BlockUser(string id)
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            bool isBlocked = await client.PostJsonAsync<bool>(ApiConstants.ApiUrl + "User/Block", id);
            User user = users.FirstOrDefault(u => u.Id == id);
            user.IsBlocked = isBlocked;
            StateHasChanged();
        }
    }
}
