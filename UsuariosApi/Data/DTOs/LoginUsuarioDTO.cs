using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Data.DTOs
{
    public class LoginUsuarioDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string Password { get; set; }
    }
}