using Microsoft.AspNetCore.Authentication.BearerToken;
using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Security.Claims;

namespace NewApp.Controllers
{
    [ApiController]
    [Route("api/autenticacao")]
    public partial class AutenticacaoController() : ControllerBase
    {
        [HttpGet, Authorize]
        public IActionResult Get() => Ok($"Olá {User.Identity.Name}");

        [HttpPost]
        public IResult Post(LoginRequest model)
        {
            if (model.password == "8318")
            {
                var claimsPrincipal = new ClaimsPrincipal(new ClaimsIdentity([new Claim(ClaimTypes.Name, "Tiago")], BearerTokenDefaults.AuthenticationScheme));
                return Results.SignIn(claimsPrincipal);
            }
            else
            {
                return Results.Ok("Login ou senha incorretos");
            }
        }

        public record LoginRequest(string password);
    }
}
