using System.Linq.Expressions;

namespace Trybe.Domain.Interfaces
{
    public interface IBaseRepository<T>
    {
        Task<T> AdicionarAsync(T entidade);
        Task<T> AtualizarAsync(T entidade);
        Task DeletarAsync(Guid id);
        Task<IEnumerable<T>> ObterTodosAsync();
        Task<T> ObterPorIdAsync(object id);

        Task<T> ObterUnicoAsync(Expression<Func<T, bool>> expression);

        Task<IEnumerable<T>> ObterAsync(Expression<Func<T, bool>> predicate);

        Task<IEnumerable<T>> ObterParaEscritaAsync(Expression<Func<T, bool>> predicate);

        Task<int> CommitAsync();
    }
}