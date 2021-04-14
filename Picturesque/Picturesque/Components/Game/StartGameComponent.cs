using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class StartGameComponent : ComponentBase
    {
        public int numberOfSelectedPictures = 0;
        public int numberOfPicturesVisible = 0;
        public int score = 0;
        public int numberOfMistakesAllowed;
        public int counter;

        public bool gameHasStarted = false;
        public bool gameHasEnded = false;
        public bool hasWon = false;
        public bool hasLost = false;
        public bool isHelpBeingUsed = false;

        public GameOptions gameOptions;
        public Picture selectedPicture = new Picture();
        public GameOptionsInputModel gameOptionsInputModel = new GameOptionsInputModel();
        public Game game = new Game();

        private Timer timer;

        protected override async Task OnInitializedAsync()
        {
            CleanUp();
            game.Pictures = new List<Picture>();

            HttpClient client = new HttpClient();
            gameOptions = await client.GetJsonAsync<GameOptions>(ApiConstants.ApiUrl + "Game/GetGameOptions");
        }

        protected async Task StartGame()
        {
            CleanUp();
            HttpClient client = new HttpClient();
            game = await client.PostJsonAsync<Game>(
                    ApiConstants.ApiUrl + "Game/StartGame",
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

        protected async Task Help()
        {
            isHelpBeingUsed = true;
            List<Picture> hiddenPictures = game.Pictures.Where(pic => !pic.IsVisible).ToList();

            switch (game.Difficulty)
            {
                case 0:
                    counter -= 10;
                    break;
                case 1:
                    counter -= 20;
                    break;
                case 2:
                    counter -= 30;
                    break;
                default:
                    break;
            }

            foreach (Picture picture in hiddenPictures)
            {
                picture.IsVisible = true;
            }

            await Task.Delay(1000);

            foreach (Picture picture in hiddenPictures)
            {
                picture.IsVisible = false;
            }

            isHelpBeingUsed = false;
        }

        private void SetCounter()
        {
            switch (game.Difficulty)
            {
                case 0:
                    counter = 60;
                    numberOfMistakesAllowed = 10;
                    break;
                case 1:
                    counter = 120;
                    numberOfMistakesAllowed = 32;
                    break;
                // TOOD: Tweak Hard mode
                case 2:
                    counter = 900;
                    numberOfMistakesAllowed = 50;
                    break;
                default:
                    break;
            }
        }

        private void StartCountdown()
        {
            timer = new Timer(new TimerCallback(_ =>
            {
                if (counter <= 0 || numberOfMistakesAllowed == 0)
                {
                    if (!hasWon)
                    {
                        gameHasEnded = true;
                        hasLost = true;
                        gameHasStarted = false;
                        timer.Dispose();
                        StateHasChanged();
                        return;
                    }
                }
                counter--;

                // Note that the following line is necessary because otherwise
                // Blazor would not recognize the state change and not refresh the UI
                this.StateHasChanged();
            }), null, 1000, 1000);
        }

        private async Task<Picture> ComparePictures(Picture picture)
        {
            await Task.Delay(250);
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

            if (timer != null)
            {
                timer.Dispose();
            }
            
            StateHasChanged();
        }
    }
}
