using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace NewApp.Controllers;

[Authorize]
[ApiController]
[Route("api/categorias")]
public class CategoriasController(IHermanosRepository respository) : ControllerBase
{
    private readonly IHermanosRepository _repository = respository;

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        var lista = await _repository.GetAll();
        return Ok(lista);
    }

    [HttpGet("{id}")]
    public async Task<IActionResult> GetBy(int id)
    {
        var model = await _repository.Get(id);
        return Ok(model);
    }


    [HttpPost]
    public async Task<IActionResult> Post(Hermano model)
    {
        if (ModelState.IsValid)
        {
            await _repository.Upsert(model);
            return Ok(model);
        }

        return Ok(ModelState);
    }
}
