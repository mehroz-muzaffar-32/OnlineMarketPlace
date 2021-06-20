using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using OnlineMarketPlaceTorico.Client.Services;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace OnlineMarketPlaceTorico.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("app");

            //This line is giving access token not available error
            //builder.Services.AddHttpClient("OnlineMarketPlaceTorico.ServerAPI", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
            //    .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            //This line is added to achieve task
            builder.Services.AddHttpClient("WebAPI.NoAuthenticationClient",
               client => client.BaseAddress = new Uri("https://localhost:44390"));

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("OnlineMarketPlaceTorico.ServerAPI"));
            builder.Services.AddTransient<ProductService>();
            builder.Services.AddApiAuthorization();

            await builder.Build().RunAsync();
        }
    }
}
