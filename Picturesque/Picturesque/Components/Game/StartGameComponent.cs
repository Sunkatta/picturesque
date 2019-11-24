using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class StartGameComponent : ComponentBase
    {
        public Game game = new Game();
        public GameOptions gameOptions;
        public int numberOfTiles = 0;
        public GameOptionsInputModel gameOptionsInputModel = new GameOptionsInputModel();

        protected override async Task OnInitializedAsync()
        {
            game.Pictures = new List<string>();

            HttpClient client = new HttpClient();
            gameOptions = await client.GetJsonAsync<GameOptions>("https://localhost:44317/api/Game/GetGameOptions");
        }

        protected async Task StartGame()
        {
            HttpClient client = new HttpClient();
            game = await client.PostJsonAsync<Game>(
                    "https://localhost:44317/api/Game/StartGame",
                    gameOptionsInputModel
                    );
        }
    }
}
