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
    public class PostController : ControllerBase
    {
        private readonly IPostService _postService;

        public PostController(IPostService _postService)
        {
            this._postService = _postService;
        }

        [HttpGet]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> Get(Guid id)
        {
            var posts = await _postService.BuscaPostPorIdAsync(id);

            return Ok(posts);
        }

        [HttpGet("user/all")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetPostsUser(Guid id)
        {
            var post = await _postService.BuscaPostsDoUsuarioAsync(id);

            return Ok(post);
        }

        [HttpGet("user/last")]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.NoContent)]
        public async Task<IActionResult> GetLastPostsUser(Guid id)
        {
            var post = await _postService.BuscaPostsDoUsuarioAsync(id);

            return Ok(post.LastOrDefault());
        }

        [HttpPost]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.Created)]
        public async Task<IActionResult> Post(Post postRequest)
        {

            var post = await _postService.AdicionaPostAsync(postRequest);

            return Ok(post);

        }

        [HttpDelete]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Guid id)
        {

            await _postService.DeletaPostAsync(id);

            return Ok();

        }

        [HttpPatch]
        [ProducesResponseType(typeof(Post), (int)HttpStatusCode.OK)]
        [ProducesResponseType((int)HttpStatusCode.OK)]
        public async Task<IActionResult> Delete(Post post)
        {

            await _postService.AtualizaPostAsync(post);

            return Ok();

        }
    }
}
