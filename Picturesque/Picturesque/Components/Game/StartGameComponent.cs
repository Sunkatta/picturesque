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
        public int completedInSeconds = 0;
        public int mistakesMade = 0;
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

        public string userId;

        private Timer timer;
        protected HttpClient client;

        protected async Task GetGameOptions()
        {
            gameOptions = await client.GetJsonAsync<GameOptions>(ApiConstants.ApiUrl + "Game/GetGameOptions");
        }

        protected async Task StartGame()
        {
            CleanUp();
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

        protected void CleanUp()
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

        protected void CalculatePlaytimeAndMistakes()
        {
            switch (game.Difficulty)
            {
                case 0:
                    completedInSeconds = GameConstants.EASY_MODE_SECONDS - counter;
                    mistakesMade = GameConstants.EASY_MODE_MISTAKES_ALLOWED - numberOfMistakesAllowed;
                    break;
                case 1:
                    completedInSeconds = GameConstants.MEDIUM_MODE_SECONDS - counter;
                    mistakesMade = GameConstants.MEDIUM_MODE_MISTAKES_ALLOWED - numberOfMistakesAllowed;
                    break;
                case 2:
                    completedInSeconds = GameConstants.HARD_MODE_SECONDS - counter;
                    mistakesMade = GameConstants.HARD_MODE_MISTAKES_ALLOWED - numberOfMistakesAllowed;
                    break;
                default:
                    break;
            }
        }

        private void SetCounter()
        {
            switch (game.Difficulty)
            {
                case 0:
                    counter = GameConstants.EASY_MODE_SECONDS;
                    numberOfMistakesAllowed = GameConstants.EASY_MODE_MISTAKES_ALLOWED;
                    break;
                case 1:
                    counter = GameConstants.MEDIUM_MODE_SECONDS;
                    numberOfMistakesAllowed = GameConstants.MEDIUM_MODE_MISTAKES_ALLOWED;
                    break;
                // TOOD: Tweak Hard mode
                case 2:
                    counter = GameConstants.HARD_MODE_SECONDS;
                    numberOfMistakesAllowed = GameConstants.HARD_MODE_MISTAKES_ALLOWED;
                    break;
                default:
                    break;
            }
        }

        private void StartCountdown()
        {
            timer = new Timer(new TimerCallback(async _ =>
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

                        await SendUserStatistics(false);

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
                await HandleSuccess();
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

        private async Task HandleSuccess()
        {
            score += 100 * numberOfMistakesAllowed;
            numberOfPicturesVisible++;
            if (numberOfPicturesVisible == game.Pictures.Count / 2)
            {
                hasWon = true;
                gameHasEnded = true;
                timer.Dispose();

                await SendUserStatistics(true);
            }
        }

        private void HandleFail(Picture picture)
        {
            selectedPicture.IsVisible = false;
            picture.IsVisible = false;
            numberOfMistakesAllowed--;
        }

        private async Task SendUserStatistics(bool hasWon)
        {
            CalculatePlaytimeAndMistakes();

            UserStatisticsInputModel userStatistics = new UserStatisticsInputModel()
            {
                UserId = userId,
                HasWon = hasWon,
                Playtime = completedInSeconds,
                NumberOfMistakes = mistakesMade,
                Score = score,
            };

            await client.PostJsonAsync(
                    ApiConstants.ApiUrl + "Statistics/UserStatistics",
                    userStatistics
                    );
        }
    }
}
