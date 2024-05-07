using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;
using Dominio.Data;

namespace App.Controllers
{
    [Authorize]
    public class GastosController(Contexto db, IGastosRepository repository) : Controller
    {
        private readonly IGastosRepository _repository = repository;
        private readonly Contexto _db = db;

        public async Task<IActionResult> Index(int categoriaId = 0, DateTime? mesAno = null)
        {
            mesAno ??= DateTime.Today;
            ViewData["mesAno"] = mesAno.Value.ToString("yyyy-MM");

            await CarregarViewDatas(categoriaId);            

            var lista = await _repository.ListAll(mesAno.Value, categoriaId);

            return View(lista);
        }

        public async Task<IActionResult> Anadir()
        {
            await CarregarViewDatas();

            var model = new Gasto
            {
                Fecha = DateTime.Today
            };

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
                await _repository.Upsert(model);
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
                await _repository.Upsert(model);
                return RedirectToAction("Index");
            }

            await CarregarViewDatas(model.CategoriaId);
            return View(model);
        }
    }
}
