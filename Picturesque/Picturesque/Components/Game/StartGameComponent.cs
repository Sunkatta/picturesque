using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class StartGameComponent : ComponentBase
    {
        public GameOptions gameOptions;
        public int numberOfTiles = 0;
        public int numberOfPicturesVisible = 0;
        public string successMessage = "";
        public Picture selectedPicture = new Picture();
        public GameOptionsInputModel gameOptionsInputModel = new GameOptionsInputModel();
        public Game game = new Game();

        protected override async Task OnInitializedAsync()
        {
            game.Pictures = new List<Picture>();

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

        protected async Task ShowPicture(Picture picture)
        {
            if (!picture.IsVisible && numberOfPicturesVisible < 2)
            {
                numberOfPicturesVisible++;
                picture.IsVisible = true;

                if (selectedPicture != null && numberOfPicturesVisible == 2)
                {
                    await Task.Delay(500);
                    if (String.Compare(picture.Img2Base64.ToLower(), selectedPicture.Img2Base64.ToLower()) == 0)
                    {
                        successMessage = "SUCCESS";
                    }
                    else
                    {
                        successMessage = "FAIL";
                        selectedPicture.IsVisible = false;
                        picture.IsVisible = false;
                    }
                    selectedPicture = null;
                    picture = null;
                    numberOfPicturesVisible = 0;
                }
                else
                {
                    selectedPicture = picture;
                    numberOfPicturesVisible = 1;
                }
            }
            
        }
    }
}
