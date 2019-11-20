using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class StartGameComponent : ComponentBase
    {
        public GameOptions gameOptions;
        public GameOptionsInputModel gameOptionsInputModel = new GameOptionsInputModel();

        protected override async Task OnInitializedAsync()
        {
            HttpClient client = new HttpClient();
            gameOptions = await client.GetJsonAsync<GameOptions>("https://localhost:44317/api/Game/GetGameOptions");
        }

        protected async Task StartGame()
        {
            HttpClient client = new HttpClient();
            await 
                client.PostJsonAsync<GameOptionsInputModel>(
                    "https://localhost:44317/api/Game/StartGame",
                    gameOptionsInputModel
                    );
        }
    }
}
