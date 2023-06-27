using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.DTOs;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {


        public UsuariosController()
        {

        }
        [HttpPost]
        public IActionResult CadastraUsuario(CreateUsuarioDTO dto)
        {
            throw new NotImplementedException();
        }

    }
}