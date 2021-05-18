using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System.Net.Http;
using System.Net.Http.Headers;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class UserStatisticsComponent : ComponentBase
    {
        protected string token;
        protected UserStatistics userStatistics;
        protected HttpClient httpClient;
        protected bool loading;

        protected async Task GetUserStatistics(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            userStatistics = await httpClient.GetJsonAsync<UserStatistics>(ApiConstants.ApiUrl + "Statistics/GetUserStatistics/" + id);
        }
    }
}
