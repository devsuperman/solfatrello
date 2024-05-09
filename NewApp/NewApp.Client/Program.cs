using Microsoft.AspNetCore.Components.WebAssembly.Hosting;
using NewApp.Client.Services;
using Dominio.Interfaces;

var builder = WebAssemblyHostBuilder.CreateDefault(args);

builder.Services.AddOptions();
builder.Services.AddAuthorizationCore();

builder.Services.AddScoped<ICategoriasRepository, CategoriasService>();
builder.Services.AddScoped<IGastosRepository, GastosService>();

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.HostEnvironment.BaseAddress)
});

await builder.Build().RunAsync();
