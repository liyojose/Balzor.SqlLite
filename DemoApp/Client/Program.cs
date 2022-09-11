using DemoApp.Client;
using DemoApp.Client.Data;
using Microsoft.AspNetCore.Components.Web;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using System.Net.Http;
using Microsoft.Extensions.Http;
using Microsoft.AspNetCore.Components.WebAssembly.Http;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.RootComponents.Add<App>("#app");
builder.RootComponents.Add<HeadOutlet>("head::after");
// Register the Message Handler
builder.Services.AddScoped(_ => new DefaultBrowserOptionsMessageHandler
{
    DefaultBrowserRequestCache = BrowserRequestCache.NoStore
});
builder.Services.AddHttpClient("Default", client => client.BaseAddress = new Uri(builder.HostEnvironment.BaseAddress))
    .AddHttpMessageHandler<DefaultBrowserOptionsMessageHandler>();
builder.Services.AddScoped(sp => new HttpClient { BaseAddress = new Uri(builder.HostEnvironment.BaseAddress) });
builder.Services.AddScoped<LocalUserStore>();
builder.Services.AddScoped(sp => sp.GetRequiredService<IHttpClientFactory>().CreateClient("Default"));
await builder.Build().RunAsync();
