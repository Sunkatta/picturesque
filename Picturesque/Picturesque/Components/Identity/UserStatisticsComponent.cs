using Blazorise.Charts;
using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using Picturesque.Models.Constants;
using System;
using System.Collections.Generic;
using System.Linq;
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
        protected LineChart<int> lineChart;
        protected List<string> Labels;
        protected List<string> backgroundColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 0.2f), ChartColor.FromRgba(54, 162, 235, 0.2f), ChartColor.FromRgba(255, 206, 86, 0.2f), ChartColor.FromRgba(75, 192, 192, 0.2f), ChartColor.FromRgba(153, 102, 255, 0.2f), ChartColor.FromRgba(255, 159, 64, 0.2f) };
        protected List<string> borderColors = new List<string> { ChartColor.FromRgba(255, 99, 132, 1f), ChartColor.FromRgba(54, 162, 235, 1f), ChartColor.FromRgba(255, 206, 86, 1f), ChartColor.FromRgba(75, 192, 192, 1f), ChartColor.FromRgba(153, 102, 255, 1f), ChartColor.FromRgba(255, 159, 64, 1f) };

        protected override async Task OnAfterRenderAsync(bool firstRender)
        {
            if (lineChart != null)
            {
                await HandleRedraw();
            }
        }

        async Task HandleRedraw()
        {
            await lineChart.Clear();
            
            Labels = Enumerable.Range(0, 7)
                .Select(i => DateTime.Now.Date.AddDays(-i))
                .Select(x => x.DayOfWeek.ToString())
                .Reverse()
                .ToList();

            await lineChart.AddLabelsDatasetsAndUpdate(Labels, GetLineChartDataset());
        }

        LineChartDataset<int> GetLineChartDataset()
        {
            return new LineChartDataset<int>
            {
                Label = "Daily Score Of All Won Games In The Past Week",
                Data = userStatistics.DailyWonGamesScore,
                BackgroundColor = backgroundColors,
                BorderColor = borderColors,
                Fill = true,
                PointRadius = 5,
                LineTension = 0,
            };
        }

        protected async Task GetUserStatistics(string id)
        {
            httpClient.DefaultRequestHeaders.Authorization = new AuthenticationHeaderValue("Bearer", token);
            userStatistics = await httpClient.GetJsonAsync<UserStatistics>(ApiConstants.ApiUrl + "Statistics/GetUserStatistics/" + id);
        }
    }
}
