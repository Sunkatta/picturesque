using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using Picturesque.Models.Enums;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class LeaderboardComponent : ComponentBase
    {
        protected List<GameScore> gameScores;
        protected List<GameScore> filteredGameScores;
        protected Difficulty selectedDifficulty;
        protected string token;
        protected string title = "Leaderboard for this week";

        protected async Task GetLeaderboard()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            gameScores = await client.GetJsonAsync<List<GameScore>>(ApiConstants.ApiUrl + "Statistics");
            filteredGameScores = gameScores
                .Where(x => x.CreatedOn.Value.ToLocalTime() > DateTime.Now.AddDays(-7))
                .ToList();

            selectedDifficulty = Difficulty.All;
        }

        protected void ShowAll(bool isSelected)
        {
            filteredGameScores = isSelected ? gameScores : gameScores
                .Where(x => x.CreatedOn.Value.ToLocalTime() > DateTime.Now.AddDays(-7))
                .ToList();
            title = isSelected ? "All time leaderboard" : "Leaderboard for this week";
        }
    }
}
