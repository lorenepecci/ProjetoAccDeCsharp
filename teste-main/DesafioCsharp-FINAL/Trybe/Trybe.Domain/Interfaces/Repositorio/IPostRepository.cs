using Trybe.Domain.Entidades;

namespace Trybe.Domain.Interfaces.Repositorio
{
    public interface IPostRepository : IBaseRepository<Post>
    {
        Task<IEnumerable<Post>> BuscaPostsPorUsuario(Guid id);
    }
}
