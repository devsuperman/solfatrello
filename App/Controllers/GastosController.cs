using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc.Rendering;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace App.Controllers
{
    [Authorize]
    public class GastosController(IGastosRepository gastosRepository, ICategoriasRepository categoriasRepository) : Controller
    {
        private readonly IGastosRepository _gastosRepository = gastosRepository;
        private readonly ICategoriasRepository _categoriasRepository;

        public async Task<IActionResult> Index(int categoriaId = 0, DateTime? mesAno = null)
        {
            mesAno ??= DateTime.Today;
            ViewData["mesAno"] = mesAno.Value.ToString("yyyy-MM");

            await CarregarViewDatas(categoriaId);

            var lista = await _gastosRepository.ListAll(mesAno.Value, categoriaId);

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
            var categorias = await _categoriasRepository.ListAll();
            ViewData["selectCategorias"] = new SelectList(categorias, "Id", "Nombre", categoriaId);
        }

        [HttpPost]
        public async Task<IActionResult> Anadir(Gasto model)
        {
            if (ModelState.IsValid)
            {
                await _gastosRepository.Upsert(model);
                return RedirectToAction("Index");
            }

            await CarregarViewDatas();
            return View(model);
        }

        public async Task<IActionResult> Editar(int id)
        {
            var model = await _gastosRepository.Get(id);

            await CarregarViewDatas(model.CategoriaId);

            return View(model);
        }

        [HttpPost]
        public async Task<IActionResult> Editar(Gasto model)
        {
            if (ModelState.IsValid)
            {
                await _gastosRepository.Upsert(model);
                return RedirectToAction("Index");
            }

            await CarregarViewDatas(model.CategoriaId);
            return View(model);
        }
    }
}
