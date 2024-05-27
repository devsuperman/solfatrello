using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.DTOs;

namespace SolfatrelloApp.Controllers;

[ApiController]
[Route("api/tareas")]
public class TareasController(ITareasRepository repository) : ControllerBase
{
    private readonly ITareasRepository _repository = repository;

    [HttpGet]
    public async Task<IActionResult> GetAll(int hermanoId = 0)
    {
        var lista = await _repository.GetAll(hermanoId);
        return Ok(lista);
    }
    
    [HttpGet("{id}")]
    public async Task<IActionResult> Get(int id)
    {
        var model = await _repository.Get(id);
        return Ok(model);
    }

    [Authorize]
    [HttpPost]
    public async Task<IActionResult> Upsert(FormTarea model)
    {
        if (ModelState.IsValid)
        {
            await _repository.Upsert(model);
            return Ok(model);
        }
        return Ok(ModelState);
    }
}
