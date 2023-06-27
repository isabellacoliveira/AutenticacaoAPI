using Microsoft.AspNetCore.Identity;

namespace UsuariosApi.Models
{
    // pode usar informações de usuario do identity
    public class Usuario : IdentityUser
    {
        // assim podemos acessar as propriedades que ja sao de usuario 
        // criando um modelo customizado, podemos adicionar propriedades a mais 
        public DateTime DataNascimento { get; set; }
        public Usuario(): base() {}   
    }
}