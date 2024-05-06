﻿using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;
using Dominio.Models;

namespace NewApp.Controllers
{
    [ApiController]
    [Route("api/categorias")]
    public class CategoriasController : ControllerBase
    {
        private readonly ICategoriasRepository _repository;

        public CategoriasController(ICategoriasRepository respository)
        {
            _repository = respository;
        }

        [HttpGet]
        public async Task<IActionResult> Get()
        {
            var lista = await _repository.ListAll();
            return Ok(lista);
        }

        [HttpGet("{id}")]
        public async Task<IActionResult> GetBy(int id)
        {
            var model = await _repository.Get(id);
            return Ok(model);
        }


        [HttpPost]
        public async Task<IActionResult> Post(Categoria model)
        {
            if (ModelState.IsValid)
            {
                await _repository.Upsert(model);
                return Ok(model);
            }

            return Ok(ModelState);
        }
    }
}
