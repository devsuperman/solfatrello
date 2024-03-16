using Microsoft.AspNetCore.Authentication.Cookies;
using Microsoft.AspNetCore.Authentication;
using Microsoft.AspNetCore.Authorization;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;
using System.Diagnostics;
using App.Models;
using App.Data;

namespace App.Controllers;

public class HomeController : Controller
{
    private readonly ILogger<HomeController> _logger;
    private readonly Contexto _db;

    public HomeController(ILogger<HomeController> logger, Contexto db)
    {
        _logger = logger;
        _db = db;
    }


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
                new Claim(ClaimTypes.Name, "Tiago"),
                new Claim(ClaimTypes.NameIdentifier, "Tiago")
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

        var totaisPorCategoria = await _db.Gastos
            .Where(w =>
                w.Fecha.Month == mesAno.Value.Month &&
                w.Fecha.Year == mesAno.Value.Year)
            .Select(s => new
            {
                Categoria = s.Categoria.Nombre,
                s.Valor
            })
            .GroupBy(g => g.Categoria)
            .Select(s => new Tuple<string, decimal>(s.Key, s.Sum(d => d.Valor.Value)))
            .ToListAsync();

        totaisPorCategoria = totaisPorCategoria.OrderByDescending(o=>o.Item2).ToList();

        return View(totaisPorCategoria);
    }

    public IActionResult Privacy()
    {
        return View();
    }

    [ResponseCache(Duration = 0, Location = ResponseCacheLocation.None, NoStore = true)]
    public IActionResult Error()
    {
        return View(new ErrorViewModel { RequestId = Activity.Current?.Id ?? HttpContext.TraceIdentifier });
    }
}
