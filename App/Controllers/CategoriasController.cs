using Microsoft.EntityFrameworkCore;
using Microsoft.AspNetCore.Mvc;
using App.Models;
using App.Data;

namespace App.Controllers
{
    public class CategoriasController : Controller
    {
        private readonly Contexto _db;

        public CategoriasController(Contexto db)
        {
            _db = db;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _db.Categorias.OrderBy(o => o.Nombre).ToListAsync();
            return View(lista);
        }

        public IActionResult Anadir() => View();

        [HttpPost]
        public async Task<IActionResult> Anadir(Categoria model)
        {
            if (ModelState.IsValid)
            {
                _db.Add(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var model = await _db.Categorias.FindAsync(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Categoria model)
        {
            if (ModelState.IsValid)
            {
                _db.Update(model);
                await _db.SaveChangesAsync();

                return RedirectToAction("Index");
            }

            return View(model);
        }


        [HttpPost]
        public async Task<IActionResult> Deletar(int id)
        {
            var model = await _db.Categorias.FindAsync(id);

            _db.Categorias.Remove(model);

            await _db.SaveChangesAsync();

            return RedirectToAction("Index");
        }
    }
}
