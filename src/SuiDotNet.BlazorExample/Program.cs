using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using SuiDotNet.Blazor;
using SuiDotNet.BlazorExample;
using SuiDotNet.Client;


var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");

builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });

builder.Services.AddSingleton<ISuiWebWallet, SuiWebWallet>();
var suiClientSettings = builder.Configuration.GetSection("SuiClient").Get<SuiClientSettings>();
builder.Services.AddSingleton<ISuiClient>(sp => new SuiJsonClient(suiClientSettings));

await builder.Build().RunAsync();
