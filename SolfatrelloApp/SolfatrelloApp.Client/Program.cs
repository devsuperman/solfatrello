using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using Microsoft.AspNetCore.Components.Authorization;
using SolfatrelloApp.Client.Services;
using Dominio.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();
builder.Services.AddCascadingAuthenticationState();

builder.Services.AddScoped<AuthenticationStateProvider, ClientAuthenticationProvider>();
builder.Services.AddScoped<ITokenStorage, LocalStorageToken>();
builder.Services.AddScoped<IAutenticacaoService, AutenticacaoService>();
builder.Services.AddScoped<IHermanosRepository, HermanosService>();
builder.Services.AddScoped<ITareasRepository, TareasService>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
