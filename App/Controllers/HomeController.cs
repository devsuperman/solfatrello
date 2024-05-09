using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using Dominio.Data;
using App.Models;
using Dominio.Interfaces;

namespace App.Controllers;

public class HomeController(ILogger<HomeController> logger, IGastosRepository gastosRepository) : Controller
{
    private readonly IGastosRepository gastosRepository = gastosRepository;
    public IActionResult Entrar() => View();

    [HttpPost]
    public async Task<IActionResult> Entrar(string senha, string returnUrl)
    {
        if (senha != "8318")
            return View();

        await Logar();

        if (!string.IsNullOrEmpty(returnUrl))
            return Redirect(returnUrl);

        return RedirectToAction("Index");
    }

    private async Task Logar()
    {
        var claims = new List<Claim>
        {
            new(ClaimTypes.Name, "Tiago"),
            new(ClaimTypes.NameIdentifier, "Tiago")
        };

        var claimsIdentity = new ClaimsIdentity(claims, CookieAuthenticationDefaults.AuthenticationScheme);
        var claimPrincipal = new ClaimsPrincipal(claimsIdentity);
        var authProperties = new AuthenticationProperties { IsPersistent = true };

        await HttpContext.SignInAsync(
            CookieAuthenticationDefaults.AuthenticationScheme,
            claimPrincipal,
            authProperties);
    }


    [Authorize]
    public async Task<IActionResult> Index(DateTime? mesAno = null)
    {
        mesAno ??= DateTime.Today;

        ViewData["mesAno"] = mesAno.Value.ToString("yyyy-MM");

        var totaisPorCategoria = await gastosRepository.ListarPorCategoria(mesAno.Value);

        return View(totaisPorCategoria);
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
