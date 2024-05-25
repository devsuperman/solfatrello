using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace App.Controllers;

[Authorize]
[ApiController]
[Route("api/tareas")]
public class TareasController(ITareasRepository repository) : ControllerBase
{
    private readonly ITareasRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll()
    {
        var lista = await _repository.GetAll();
        return Ok(lista);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var model = await _repository.Get(id);
        return Ok(model);
    }

    [HttpPost]
    public async Task<IActionResult> Upsert(Tarea model)
    {
        if (ModelState.IsValid)
        {
            await _repository.Upsert(model);
            return Ok(model);
        }
        return Ok(ModelState);
    }
}
