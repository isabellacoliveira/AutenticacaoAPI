    using System.IdentityModel.Tokens.Jwt;
    using System.Security.Claims;
    using System.Text;
    using Microsoft.IdentityModel.Tokens;
    using UsuariosApi.Models;

    namespace UsuariosApi.Services
    {
        public class TokenService
        {
        private IConfiguration _configuration;

        public TokenService(IConfiguration configuration)
            {
                _configuration = configuration;
            }
            public string GenerateToken(Usuario usuario)
            {
                Claim[] claims = new Claim[]
                {
                    // vamos usar as claims pra gerar o token
                    new Claim("username", usuario.UserName),
                    new Claim("id", usuario.Id),
                    new Claim(ClaimTypes.DateOfBirth, usuario.DataNascimento.ToString()),
                    // mostra o momento que o login foi feito
                    new Claim("loginTimestamp", DateTime.UtcNow.ToString())
                };
                // operação que precisamos realizar a partir de uma chave 
                // cadeia de caracteres que vao representar essa chave  
                var chave  = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["SymmetricSecurityKey"]));
                var signingCredentials = new SigningCredentials(chave, SecurityAlgorithms.HmacSha256);

                var token = new JwtSecurityToken
                (
                    expires: DateTime.Now.AddMinutes(10),
                    claims: claims,
                    signingCredentials: signingCredentials
                );
                // precisamos converter o token para uma string 
                return new JwtSecurityTokenHandler().WriteToken(token);
            }
        }
    }