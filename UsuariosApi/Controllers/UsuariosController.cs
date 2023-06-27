using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private UsuarioService _usuarioService;
        public UsuariosController(UsuarioService usuarioService)
        {
            _usuarioService = usuarioService;
        }
        [HttpPost("cadastro")]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDTO dto)
        {
            await _usuarioService.Cadastra(dto);
            return Ok("Usuário cadastrado!");
        }

        [HttpPost("login")]
        public async Task<IActionResult> Login(LoginUsuarioDTO dto)
        {
            var token = await _usuarioService.Login(dto);
            return Ok($"Usuário autenticado! Token {token}");
        }

    }
}