using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using Picturesque.Models.Enums;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class LeaderboardComponent : ComponentBase
    {
        public GameScore[] gameScores;
        public Difficulty selectedDifficulty;
        public string token;

        public async Task GetLeaderboard()
        {
            HttpClient client = new HttpClient();
            client.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            gameScores = await client.GetJsonAsync<GameScore[]>(ApiConstants.ApiUrl + "Statistics");
            selectedDifficulty = Difficulty.All;
        }
    }
}
