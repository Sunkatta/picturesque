using Microsoft.AspNetCore.Components;
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

        public class WeatherForecast
        {
            public DateTime Date { get; set; }

            public int TemperatureC { get; set; }

            public string Summary { get; set; }

            public int TemperatureF => 32 + (int)(TemperatureC / 0.5556);
        }
    }
}
