using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.EntityFrameworkCore;
using Dominio.Repositories;
using Dominio.Interfaces;
using NewApp.Extensions;
using Dominio.Data;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddRazorComponents()
    .AddInteractiveServerComponents()
    .AddInteractiveWebAssemblyComponents();

builder.Services.AddControllers();

builder.Services.AddCascadingAuthenticationState();

builder.Services.AddAuthentication(CookieAuthenticationDefaults.AuthenticationScheme)
    .AddCookie(x =>
    {
        x.LoginPath = "/Login";
        x.LogoutPath = "/Login";
        x.ExpireTimeSpan = new TimeSpan(5, 0, 0, 0);
    });


builder.Services.AddScoped<ICategoriasRepository, CategoriasRepository>();
builder.Services.AddScoped<IGastosRepository, GastosRepository>();

builder.Services.AddDbContext<Contexto>(
        options => options.UseNpgsql(ConnectionHelper.GetConnectionString(builder.Configuration),
        a => a.MigrationsAssembly("NewApp")));

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

app.UsarCulturaEspecifica("es-ES");

app.UseHttpsRedirection();
app.UseStaticFiles();
app.UseAntiforgery();
app.MapControllers();

app.MapRazorComponents<NewApp.Components.App>()
    .AddInteractiveServerRenderMode()
    .AddInteractiveWebAssemblyRenderMode()
    .AddAdditionalAssemblies(typeof(NewApp.Client._Imports).Assembly);

app.Run();
