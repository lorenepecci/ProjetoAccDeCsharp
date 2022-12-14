using Trybe.Domain.Entidades;

namespace Trybe.Domain.Interfaces.Aplicacao
{
    public interface IPostService
    {
        Task<Post> BuscaPostPorIdAsync(Guid id);
        Task<IEnumerable<Post>> BuscaPostsDoUsuarioAsync(Guid id);
        Task<Post> AtualizaPostAsync(Post post);
        Task DeletaPostAsync(Guid id);
        Task<bool> AdicionaPostAsync(Post post);
    }
}
