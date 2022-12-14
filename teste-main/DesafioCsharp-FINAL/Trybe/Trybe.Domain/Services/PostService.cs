using Trybe.Domain.Entidades;
using Trybe.Domain.Interfaces.Aplicacao;
using Trybe.Domain.Interfaces.Repositorio;

namespace Trybe.Domain.Services
{
    public class PostService : IPostService
    {
        private readonly IPostRepository _postRepository;

        public PostService(IPostRepository postRepository)
        {
            _postRepository = postRepository;
        }

        public async Task<bool> AdicionaPostAsync(Post post)
        {
            var retornoPost = await _postRepository.AdicionarAsync(post);

            return retornoPost != null;
        }

        public async Task<Post> AtualizaPostAsync(Post post)
        {
            var postAntigo = await _postRepository.ObterPorIdAsync(post.Id);
            postAntigo.Titulo = post.Titulo;
            postAntigo.Texto = post.Texto;

            var postNovo = await _postRepository.AtualizarAsync(postAntigo);

            return postNovo;
        }

        public async Task<Post> BuscaPostPorIdAsync(Guid id)
        {
            var post = await _postRepository.ObterUnicoAsync(item => item.Id == id);

            if (post == null)
                return new Post();

            return post;
        }

        public async Task<IEnumerable<Post>> BuscaPostsDoUsuarioAsync(Guid id)
        {
            var posts = await _postRepository.BuscaPostsPorUsuario(id);

            //if (posts == null)
            //    return new List { };

            return posts;
        }

        public async Task DeletaPostAsync(Guid id) =>
            await _postRepository.DeletarAsync(id);

    }
}
