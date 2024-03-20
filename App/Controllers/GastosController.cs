using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Data;

namespace App.Controllers
{
    [Authorize]
    public class GastosController : Controller
    {
        private readonly Contexto _db;

        public GastosController(Contexto db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index(int categoriaId = 0, DateTime? mesAno = null)
        {
            mesAno ??= DateTime.Today;

            var listaCategorias = await _db.Categorias.AsNoTracking().OrderBy(o=>o.Nombre).ToListAsync();

            ViewData["selectCategorias"]= new SelectList(listaCategorias, "Id", "Nombre", categoriaId);
            ViewData["mesAno"] = mesAno.Value.ToString("yyyy-MM");

            var query = _db.Gastos
                .AsNoTrackingWithIdentityResolution()
                .Include(a => a.Categoria)
                .Where(w =>
                    w.Fecha.Month == mesAno.Value.Month &&
                    w.Fecha.Year == mesAno.Value.Year);

            if (categoriaId > 1)
                query = query.Where(w => w.CategoriaId == categoriaId);

            var lista = await query
                .OrderByDescending(o => o.Fecha)
                .ToListAsync();

            return View(lista);
        }

        public async Task<IActionResult> Anadir()
        {
            await CarregarViewDatas();

            var model = new Gasto
            {
                Fecha = DateTime.Today
            };

            await CarregarViewDatas();

            return View(model);
        }

        private async Task CarregarViewDatas(int categoriaId = 0)
        {
            var categorias = await _db.Categorias
                .AsNoTracking()
                .OrderBy(o => o.Nombre)
                .Select(s => new
                {
                    s.Id,
                    s.Nombre
                })
                .ToListAsync();

            ViewData["selectCategorias"] = new SelectList(categorias, "Id", "Nombre", categoriaId);
        }

        [HttpPost]
        public async Task<IActionResult> Anadir(Gasto model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }


            await CarregarViewDatas();

            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var model = await _db.Gastos.FindAsync(id);

            await CarregarViewDatas(model.CategoriaId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Gasto model)
        {
            if (ModelState.IsValid)
            {
                _db.Update(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            await CarregarViewDatas(model.CategoriaId);
            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var model = await _db.Gastos.FindAsync(id);

            _db.Gastos.Remove(model);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
