using AutoMapper;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Models;
using UsuariosApi.Services;

namespace UsuariosApi.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UsuariosController : ControllerBase
    {
        private CadastroService _cadastroService;
        public UsuariosController(CadastroService cadastroService)
        {
            _cadastroService = cadastroService;
        }
        [HttpPost]
        public async Task<IActionResult> CadastraUsuario(CreateUsuarioDTO dto)
        {
            await _cadastroService.Cadastra(dto);
            return Ok("Usuário cadastrado!");
        }

    }
}