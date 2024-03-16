using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
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
            .OrderByDescending(o => o.Item2)
            .ToListAsync();

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
