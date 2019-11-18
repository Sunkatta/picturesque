using Microsoft.AspNetCore.Components;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class StartGameComponent : ComponentBase
    {
        public GameOptions gameOptions;

        protected override async Task OnInitializedAsync()
        {
            HttpClient client = new HttpClient();
            gameOptions = await client.GetJsonAsync<GameOptions>("https://localhost:44317/api/Game/GetGameOptions");
        }

        protected async Task StartGame()
        {

        }

        public class GameOptions
        {
            public string[] Categories { get; set; }
            public string[] Difficulties { get; set; }
        }
    }
}
