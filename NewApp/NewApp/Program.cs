using Microsoft.EntityFrameworkCore;
using Dominio.Interfaces;
using NewApp.Components;
using NewApp.Services;
using Dominio.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()    
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();
builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();

builder.Services.AddDbContext<Contexto>(options =>
    options.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration)));

builder.Services.AddScoped(http => new HttpClient
{
    BaseAddress = new Uri(builder.Configuration.GetSection("BaseAddress").Value!)
});

var portVar = Environment.GetEnvironmentVariable("PORT");

if (portVar is { Length: > 0 } && int.TryParse(portVar, out int port))
{
    builder.WebHost.ConfigureKestrel(options =>
    {
        options.ListenAnyIP(port);
    });
}

var app = builder.Build();

if (app.Environment.IsDevelopment())
{
    app.UseWebAssemblyDebugging();
}
else
{
    app.UseExceptionHandler("/Error", createScopeForErrors: true);
    app.UseHsts();
}

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NewApp.Client._Imports).Assembly);

app.Run();
