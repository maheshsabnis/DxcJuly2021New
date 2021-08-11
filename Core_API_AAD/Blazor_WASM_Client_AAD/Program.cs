using Microsoft.AspNetCore.Components.WebAssembly.Authentication;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.Extensions.Configuration;
using Microsoft.Extensions.DependencyInjection;
using Microsoft.Extensions.Logging;
using System;
using System.Collections.Generic;
using System.Net.Http;
using System.Text;
using System.Threading.Tasks;

namespace Blazor_WASM_Client_AAD
{
    public class Program
    {
        public static async Task Main(string[] args)
        {
            var builder = WebAssemblyHostBuilder.CreateDefault(args);
            builder.RootComponents.Add<App>("#app");

            builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

            builder.Services.AddHttpClient("Blazor_WASM_Client_AAD.ServerAPI", client =>
                 client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
             .AddHttpMessageHandler<BaseAddressAuthorizationMessageHandler>();

            builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>()
                .CreateClient("Blazor_WASM_Client_AAD.ServerAPI"));



            // Microsoft Libbrary for Authentication using
            // Azure AD for the SIngle PAge Client APps
            // Blazor Web Assembly, Anguar/React/Vue, etc.
            builder.Services.AddMsalAuthentication(options =>
            {
                builder.Configuration.Bind("AzureAd", options.ProviderOptions.Authentication);
                // the Token Issued by the AAD will be used 
                // byt the Blazor Application to Access ther API
                options.ProviderOptions.DefaultAccessTokenScopes.Add("api://d5ba56ff-c309-4902-bf17-464200a2be47/access_api_user");
                options.ProviderOptions.LoginMode = "redirect";
            });

            await builder.Build().RunAsync();
        }
    }
}
