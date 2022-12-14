using Microsoft.EntityFrameworkCore;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Trybe.Domain.Context;
using Trybe.Domain.Entidades;
using Trybe.Domain.Interfaces.Repositorio;

namespace Trybe.Repository.Repositories
{
    public class PostRepository : BaseRepository<Post>, IPostRepository
    {

        protected RedeSocialContext _db;
        public PostRepository(RedeSocialContext db) : base(db) {
            _db = db;
        }

        public async Task<IEnumerable<Post>> BuscaPostsPorUsuario(Guid id)
        {
            var result = _db.Set<Post>().AsNoTracking().Where(item => item.IdUsuario == id);
            return await result.ToListAsync().ConfigureAwait(false);
        }

    }
}
