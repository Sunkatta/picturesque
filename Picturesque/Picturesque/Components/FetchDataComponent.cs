using Microsoft.AspNetCore.Components;
using Picturesque.Models;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Net.Http;
using System.Threading.Tasks;

namespace Picturesque.Components
{
    public class FetchDataComponent : ComponentBase
    {
        public WeatherForecast[] forecasts;

        protected override async Task OnInitializedAsync()
        {
            HttpClient client = new HttpClient();
            forecasts = await client.GetJsonAsync<WeatherForecast[]>("https://localhost:44317/api/weatherforecast");
        }
    }
}
