using System.ComponentModel.DataAnnotations;
using Microsoft.AspNetCore.Mvc;

namespace UsuariosApi.Data.DTOs
{
    public class CreateUsuarioDTO
    {
        [Required]
        public string Username { get; set; }
        [Required]
        public string DataNascimento { get; set; }
        [Required]
        [DataType(DataType.Password)]
        public string Password { get; set; }
        [Required]
        [Compare("Password")]
        public string RePassword { get; set; }
    }
}