using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text.Json;
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

        protected void ShowPicture(Picture picture)
        {
            numberOfPicturesVisible++;
            picture.IsVisible = !picture.IsVisible;

            if(selectedPicture != null && numberOfPicturesVisible == 2 && picture.IsVisible)
            {
                if(String.Compare(picture.Img2Base64.ToLower(), selectedPicture.Img2Base64.ToLower()) == 0)
                {
                    successMessage = "SUCCESS";
                } else
                {
                    successMessage = "FAIL";
                    selectedPicture.IsVisible = false;
                    picture.IsVisible = false;
                }
                selectedPicture = null;
                numberOfPicturesVisible = 0;
            } else
            {
                selectedPicture = picture;
            }
        }
    }
}
