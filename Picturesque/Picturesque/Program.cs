using Blazorise;
using Blazorise.Bootstrap;
using MatBlazor;
using Microsoft.AspNetCore.Components.Authorization;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using Tewr.Blazor.FileReader;

namespace Picturesque
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
            builder.Services.AddAuthorizationCore();
            builder.Services.AddScoped<TokenAuthenticationStateProvider>();
            builder.Services.AddScoped<AuthenticationStateProvider>(provider => provider.GetRequiredService<TokenAuthenticationStateProvider>());
            builder.Services.AddFileReaderService(options => options.UseWasmSharedBuffer = true);
            builder.Services.AddMatBlazor();
            builder.Services.AddMatToaster(config =>
            {
                config.Position = MatToastPosition.BottomLeft;
                config.PreventDuplicates = true;
                config.NewestOnTop = true;
                config.ShowCloseButton = true;
                config.MaximumOpacity = 100;
                config.VisibleStateDuration = 3000;
            });
            builder.Services.AddBlazorise().AddBootstrapProviders();

            await builder.Build().RunAsync();
        }

    }
}
