using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[Controller]")]
    public class AcessoController : ControllerBase
    {
        // validar nosso acesso no momento do login 
        // precisamos colocar uma condição de autorização
        [HttpGet]
        [Authorize(Policy = "IdadeMinima")]
        public IActionResult Get() 
        {
            return Ok("Acesso permitido!");
        }

    }
}