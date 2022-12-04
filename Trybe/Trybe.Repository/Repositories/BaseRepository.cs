using Microsoft.EntityFrameworkCore;
using System.Diagnostics.CodeAnalysis;
using System.Linq.Expressions;
using Trybe.Domain.Context;
using Trybe.Domain.Interfaces;

namespace Trybe.Repository.Repositories
{
    [ExcludeFromCodeCoverage]
    public class BaseRepository<T> : IDisposable, IBaseRepository<T> where T : class, new()
    {
        protected RedeSocialContext _db;
        protected DbSet<T> _dbSet;
        public BaseRepository(RedeSocialContext db)
        {
            _db = db;
            _dbSet = _db.Set<T>();
        }
        public void DetashLocal(Func<T, bool> predicate)
        {
            var local = _db.Set<T>().Local.FirstOrDefault(predicate);
            if (local != null)
            {
                _db.Entry(local).State = EntityState.Detached;
            }
        }

        public async Task<T> AdicionarAsync(T entidade)
        {
            await _db.Set<T>().AddAsync(entidade);
            _db.SaveChanges();
            return entidade;
        }

        public async Task<T> AtualizarAsync(T entidade)
        {
            _db.Set<T>().Update(entidade);
            _db.SaveChanges();
            return entidade;
        }
        public async Task<T> Update(T updated, long key)
        {
            if (updated == null)
                return null;


            T existing = await _db.Set<T>().FindAsync(key);
            if (existing != null)
            {

                _db.Entry(existing).CurrentValues.SetValues(updated);
            }
            return existing;
        }
        public async Task DeletarAsync(Guid id)
        {
            var entidade = await ObterPorIdAsync(id).ConfigureAwait(false);

            _db.Set<T>().Remove(entidade);
            _db.SaveChanges();
        }
        public async Task Truncate()
        {
            var name = _db.Model.FindEntityType(typeof(T));

            await _db.Database
                .ExecuteSqlRawAsync($"truncate table {name.Name}")
                .ConfigureAwait(false);
        }

        public virtual async Task<T> ObterPorIdAsync(object id)
        {
            T result = await _db.Set<T>().FindAsync(id);
            return result;
        }

        public virtual async Task<IEnumerable<T>> ObterTodosAsync()
        {

            return await _db.Set<T>().ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<T>> ObterAsync(Expression<Func<T, bool>> predicate)
        {
            var result = _db.Set<T>().AsNoTracking().Where(predicate);
            return await result.ToListAsync().ConfigureAwait(false);
        }

        public virtual async Task<IEnumerable<T>> ObterParaEscritaAsync(Expression<Func<T, bool>> predicate)
        {
            var result = _db.Set<T>().Where(predicate);
            return await result.ToListAsync().ConfigureAwait(false);
        }

        public void Dispose()
        {

            _db?.Dispose();
            GC.SuppressFinalize(this);
        }

        public async Task<T> ObterUnicoAsync(Expression<Func<T, bool>> expression)
        {
            var result = await _db.Set<T>()
                .SingleOrDefaultAsync(expression)
                .ConfigureAwait(false);

            return result;
        }


        public async Task<int> CommitAsync()
        {
            var retorno = await _db.SaveChangesAsync().ConfigureAwait(false);
            foreach (var entity in _db.ChangeTracker.Entries())
                entity.State = EntityState.Detached;
            return retorno;
        }
    }
}