// Program.cs
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using ShopEase.Client;
using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using System;
using System.Net.Http;
using System.Threading.Tasks;
using ShopEase.Client.Services; // Add this line

namespace ShopEase.Client
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");
            builder.RootComponents.Add<HeadOutlet>("head::after");

            // Configure HttpClient with base address from appsettings.json
            builder.Services.AddHttpClient("ShopEaseAPI", client =>
            {
                client.BaseAddress = new Uri(builder.Configuration["ApiBaseUrl"] ?? throw new InvalidOperationException("ApiBaseUrl not found in appsettings.json"));
            })
            .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            // Supply HttpClient instances that include access tokens when making requests to the server project
            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
            .CreateClient("ShopEaseAPI"));

            builder.Services.AddOidcAuthentication(options =>
            {
                options.ProviderOptions.ClientId = "ShopEase.Client";
                options.ProviderOptions.Authority = builder.Configuration["AuthAuthority"];
                options.ProviderOptions.DefaultScopes.Add("api1");
                options.ProviderOptions.ResponseType = "code";
            });

            builder.Services.AddScoped<INotificationService, NotificationService>(); // Add this line

            await builder.Build().RunAsync();
        }
    }
}