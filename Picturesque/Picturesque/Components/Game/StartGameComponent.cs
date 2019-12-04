using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Text.Json;
using System.Threading;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class StartGameComponent : ComponentBase
    {
        public int numberOfSelectedPictures = 0;
        public int numberOfPicturesVisible = 0;
        public int score = 0;
        public int numberOfMistakesAllowed = 10;
        public int counter;

        public bool gameHasStarted = false;
        public bool gameHasEnded = false;
        public bool hasWon = false;
        public bool hasLost = false;

        public GameOptions gameOptions;
        public Picture selectedPicture = new Picture();
        public GameOptionsInputModel gameOptionsInputModel = new GameOptionsInputModel();
        public Game game = new Game();

        protected override async Task OnInitializedAsync()
        {
            CleanUp();
            game.Pictures = new List<Picture>();

            HttpClient client = new HttpClient();
            gameOptions = await client.GetJsonAsync<GameOptions>("https://localhost:44317/api/Game/GetGameOptions");
        }

        protected async Task StartGame()
        {
            CleanUp();
            HttpClient client = new HttpClient();
            game = await client.PostJsonAsync<Game>(
                    "https://localhost:44317/api/Game/StartGame",
                    gameOptionsInputModel
                    );

            gameHasStarted = true;
            SetCounter();
            StartCountdown();
        }

        protected async Task ShowPicture(Picture picture)
        {
            if (numberOfMistakesAllowed != 0 && !gameHasEnded)
            {
                if (!picture.IsVisible && numberOfSelectedPictures < 2)
                {
                    numberOfSelectedPictures++;
                    picture.IsVisible = true;

                    if (selectedPicture != null && numberOfSelectedPictures == 2)
                    {
                        picture = await ComparePictures(picture);
                    }
                    else
                    {
                        selectedPicture = picture;
                        numberOfSelectedPictures = 1;
                    }
                }
            }
        }

        private void SetCounter()
        {
            switch (game.Difficulty)
            {
                case 0: counter = 300;
                    break;
                case 1: counter = 600;
                    break;
                case 2: counter = 900;
                    break;
                default:
                    break;
            }
        }

        private void StartCountdown()
        {
            var timer = new Timer(new TimerCallback(_ =>
            {
                if (counter <= 0 || numberOfMistakesAllowed == 0)
                {
                    gameHasEnded = true;
                    hasLost = true;
                    gameHasStarted = false;
                    return;
                }
                counter--;

                // Note that the following line is necessary because otherwise
                // Blazor would not recognize the state change and not refresh the UI
                this.StateHasChanged();
            }), null, 1000, 1000);
        }

        private async Task<Picture> ComparePictures(Picture picture)
        {
            await Task.Delay(500);
            if (String.Compare(picture.Img2Base64.ToLower(), selectedPicture.Img2Base64.ToLower()) == 0)
            {
                HandleSuccess();
            }
            else
            {
                HandleFail(picture);
            }
            selectedPicture = null;
            picture = null;
            numberOfSelectedPictures = 0;
            return picture;
        }

        private void HandleSuccess()
        {
            score += 100 * numberOfMistakesAllowed;
            numberOfPicturesVisible++;
            if (numberOfPicturesVisible == game.Pictures.Count / 2)
            {
                hasWon = true;
                gameHasEnded = true;
                counter = 0;
            }
        }

        private void HandleFail(Picture picture)
        {
            selectedPicture.IsVisible = false;
            picture.IsVisible = false;
            numberOfMistakesAllowed--;
        }

        private void CleanUp()
        {
            gameHasStarted = false;
            gameHasEnded = false;
            hasWon = false;
            hasLost = false;
            score = 0;
            numberOfMistakesAllowed = 10;
            numberOfPicturesVisible = 0;
        }
    }
}
