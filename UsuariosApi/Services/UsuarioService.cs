using AutoMapper;
using Microsoft.AspNetCore.Identity;
using UsuariosApi.Data.DTOs;
using UsuariosApi.Models;

namespace UsuariosApi.Services
{
    public class UsuarioService
    {
        private IMapper _mapper;
        private UserManager<Usuario> _userManager;
        private SignInManager<Usuario> _signInManager;
        private TokenService _tokenService;

        public UsuarioService(IMapper mapper,UserManager<Usuario> userManager, SignInManager<Usuario> signInManager, TokenService tokenService)
        { 
            _mapper = mapper; 
            _userManager = userManager;
            _signInManager = signInManager;
            _tokenService = tokenService;
        }

        public async Task Cadastra(CreateUsuarioDTO dto)
        {
            Usuario usuario = _mapper.Map<Usuario>(dto);
            IdentityResult resultado = await _userManager.CreateAsync(usuario, dto.Password);
            
            if(!resultado.Succeeded)
            {
                throw new ApplicationException("Falha ao cadastrar usuário");
            }
        }

        public async Task<string> Login(LoginUsuarioDTO dto)
        {   
            // motivo dos falses 
            // nós nao queremos que o cookie permaneça no navegador apos ele fechar 
            // nao queremos que a conta seja bloqueada em caso de falhas
            var resultado = await _signInManager.PasswordSignInAsync(dto.Username, dto.Password, false, false);

            if(!resultado.Succeeded)
            {
                throw new ApplicationException("Usuário não autenticado!");
            }
            // temos que pegar o usuario
            var usuario = _signInManager
                                .UserManager
                                .Users
                                // primeiro usuario que me aparecer onde 
                                .FirstOrDefault(user => user.NormalizedUserName == dto.Username.ToUpper());
           var token =_tokenService.GenerateToken(usuario);
            return token;
        }
    }
}