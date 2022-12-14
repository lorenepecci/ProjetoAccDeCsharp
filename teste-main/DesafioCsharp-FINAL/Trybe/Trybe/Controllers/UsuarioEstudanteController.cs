using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using System.Net;
using Trybe.Domain.Entidades;
using Trybe.Domain.Interfaces.Aplicacao;

namespace Trybe.Controllers
{
    [ApiController]
    [Route("[controller]")]
    [Authorize("Bearer")]
    public class UsuarioEstudanteController : ControllerBase
    {
        private readonly IUsuarioService _estudanteService;

        public UsuarioEstudanteController(IUsuarioService _estudanteService)
        {
            this._estudanteService = _estudanteService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            var estudante = await _estudanteService.BuscaUsuarioPorIdAsync(id);

            return Ok(estudante);

        }

        [HttpPost]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(Usuario usuario)
        {

            var estudante = await _estudanteService.AdicionaUsuarioAsync(usuario);

            return Ok(estudante);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {

            await _estudanteService.DeletaUsuarioAsync(id);

            return Ok();

        }

        [HttpPatch]
        [ProducesResponseType(typeof(Usuario), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Usuario usuario)
        {

            await _estudanteService.AtualizaUsuarioAsync(usuario);

            return Ok();

        }
    }
}
