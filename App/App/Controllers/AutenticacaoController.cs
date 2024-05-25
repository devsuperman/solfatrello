﻿using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using Dominio.Interfaces;

namespace App.Controllers
{
    [ApiController]
    [Route("api/autenticacao")]
    public partial class AutenticacaoController(IAutenticacaoService autenticacaoService) : ControllerBase
    {
        private readonly IAutenticacaoService _autenticacaoService = autenticacaoService;

        [HttpGet, Authorize]
        public IActionResult Get() => Ok($"Olá {User.Identity.Name}");

        [HttpPost]
        public async Task<IActionResult> Post(LoginRequest model)
        {
            var response = await _autenticacaoService.LoginAsync(model.password);

            if (response.Success)
                return Ok(response);

            return Ok(response);
        }

        public record LoginRequest(string password);
    }
}
