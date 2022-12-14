using Trybe.Domain.Entidades;

namespace Trybe.Domain.Interfaces.Aplicacao
{
    public interface IUsuarioService
    {
        Task<Usuario> BuscaUsuarioPorNomeAsync(string nome);
        Task<Usuario> BuscaUsuarioPorIdAsync(Guid id);
        Task<Usuario> AtualizaUsuarioAsync(Usuario usuario);
        Task DeletaUsuarioAsync(Guid id);
        Task<bool> AdicionaUsuarioAsync(Usuario usuario);
    }
}
