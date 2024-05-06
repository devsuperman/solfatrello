using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace App.Controllers
{
    [Authorize]
    public class CategoriasController : Controller
    {
        private readonly ICategoriasRepository _repository;

        public CategoriasController(ICategoriasRepository respository)
        {
            _repository = respository;
        }

        public async Task<IActionResult> Index()
        {
            var lista = await _repository.ListAll();
            return View(lista);
        }

        public IActionResult Anadir() => View();

        [HttpPost]
        public async Task<IActionResult> Anadir(Categoria model)
        {
            if (ModelState.IsValid)
            {
                await _repository.Upsert(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var model = await _repository.Get(id);
            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Categoria model)
        {
            if (ModelState.IsValid)
            {
                await _repository.Upsert(model);
                return RedirectToAction("Index");
            }

            return View(model);
        }
    }
}
