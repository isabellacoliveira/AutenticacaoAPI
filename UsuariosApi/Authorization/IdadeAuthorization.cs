using System.Security.Claims;
using Microsoft.AspNetCore.Authorization;

namespace UsuariosApi.Authorization
{
    // gerenciador de autorizaçao
    public class IdadeAuthorization : AuthorizationHandler<IdadeMinima>
    {
        protected override Task HandleRequirementAsync(AuthorizationHandlerContext context, IdadeMinima requirement)
        {
            // atraves do contexto podemos acessar as infos do usuario e fazer a comparação 
            var dataNascimentoClaim = context
            // pegar a claim de data de nascimento
                        .User.FindFirst(claim => claim.Type == ClaimTypes.DateOfBirth);
            if(dataNascimentoClaim is null) return Task.CompletedTask;

            // se nao for nulo vamos converter para date time 
            var dataNascimento = Convert.ToDateTime(dataNascimentoClaim.Value);

            // calcular a idade 
            var idadeUsuario = DateTime.Today.Year - dataNascimento.Year;

            if(dataNascimento > DateTime.Today.AddYears(-idadeUsuario))
            {
                idadeUsuario--;
            }

            if(idadeUsuario >= requirement.Idade) context.Succeed(requirement);

            return Task.CompletedTask;

        }
    }
}