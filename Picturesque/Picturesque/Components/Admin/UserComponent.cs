using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;

namespace Picturesque.Components
{
    public class UserComponent : ComponentBase
    {
        public int counter = 1;

        public List<User> users;

        public async void BlockUser(string id)
        {
            HttpClient client = new HttpClient();
            bool isBlocked = await client.PostJsonAsync<bool>(ApiConstants.ApiUrl + "User/Block", id);
            User user = users.FirstOrDefault(u => u.Id == id);
            user.IsBlocked = isBlocked;
            StateHasChanged();
        }
    }
}
