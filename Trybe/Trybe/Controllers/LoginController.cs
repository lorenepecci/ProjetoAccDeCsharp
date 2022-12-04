using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trybe.Domain.Services;

namespace Trybe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class LoginController : ControllerBase
    {
        [AllowAnonymous]
        [HttpPost]
        [ProducesResponseType(typeof(Token), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Unauthorized)]
        public async Task<ActionResult<Token>> Post(
        [FromBody] User usuario,
        [FromServices] ILogger<LoginController> logger,
        [FromServices] GerenciadorAcesso accessManager)
        {
            logger.LogInformation($"Recebida solicitação para o usuário: {usuario?.UserID}");

            if (usuario is not null && await accessManager.ValidateCredentials(usuario))
            {
                logger.LogInformation($"Sucesso na autenticação do usuário: {usuario.UserID}");
                return accessManager.GenerateToken(usuario);
            }
            else
            {
                logger.LogError($"Falha na autenticação do usuário: {usuario?.UserID}");
                return new UnauthorizedResult();
            }
        }
    }
}
