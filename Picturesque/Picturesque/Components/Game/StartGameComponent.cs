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
        public const int EASY_MODE_SECONDS = 60;
        public const int EASY_MODE_MISTAKES_ALLOWED = 10;
        public const int MEDIUM_MODE_SECONDS = 120;
        public const int MEDIUM_MODE_MISTAKES_ALLOWED = 32;
        public const int HARD_MODE_SECONDS = 900;
        public const int HARD_MODE_MISTAKES_ALLOWED = 50;

        public int numberOfSelectedPictures = 0;
        public int numberOfPicturesVisible = 0;
        public int score = 0;
        public int numberOfMistakesAllowed;
        public int counter;

        public bool gameHasStarted;
        public bool gameHasEnded;
        public bool hasWon;
        public bool hasLost;
        public bool isHelpBeingUsed;
        public bool isHelpUsed;

        public GameOptions gameOptions;
        public Picture selectedPicture = new Picture();
        public GameOptionsInputModel gameOptionsInputModel = new GameOptionsInputModel();
        public Models.Game game = new Models.Game();

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
            game = await client.PostJsonAsync<Models.Game>(
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
            isHelpUsed = true;
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
                    counter = EASY_MODE_SECONDS;
                    numberOfMistakesAllowed = EASY_MODE_MISTAKES_ALLOWED;
                    break;
                case 1:
                    counter = MEDIUM_MODE_SECONDS;
                    numberOfMistakesAllowed = MEDIUM_MODE_MISTAKES_ALLOWED;
                    break;
                // TOOD: Tweak Hard mode
                case 2:
                    counter = HARD_MODE_SECONDS;
                    numberOfMistakesAllowed = HARD_MODE_MISTAKES_ALLOWED;
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
                timer.Dispose();
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
            isHelpUsed = false;
            score = 0;
            numberOfPicturesVisible = 0;

            if (timer != null)
            {
                timer.Dispose();
            }
            
            StateHasChanged();
        }
    }
}
