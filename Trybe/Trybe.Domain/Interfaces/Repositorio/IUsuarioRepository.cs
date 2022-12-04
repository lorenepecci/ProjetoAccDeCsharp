using Trybe.Domain.Entidades;

namespace Trybe.Domain.Interfaces.Repositorio
{
    public interface IUsuarioRepository : IBaseRepository<Usuario>
    {
        Task<Usuario> BuscaUsuarioAsync(string? nome, Guid? id);
        Task<Usuario> AtualizaUsuarioAsync(Usuario usuario);
        Task<bool> DeletaUsuarioAsync(Guid id);
    }
}
