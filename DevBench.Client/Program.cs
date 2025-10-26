using DevBench.Client;
using DevBench.Client.Services;
using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using MudBlazor.Services;

var builder = WebAssemblyHostBuilder.CreateDefault(args);
builder.Services.AddMudServices();
builder.Services.AddSingleton<SettingsService>();
builder.Services.AddLocalization(options => options.ResourcesPath = Constants.ResourcesPath);

await builder.Build().RunAsync();
