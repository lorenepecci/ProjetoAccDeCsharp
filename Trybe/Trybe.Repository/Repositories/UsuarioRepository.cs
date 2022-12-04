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
    public class UsuarioRepository : BaseRepository<Usuario>, IUsuarioRepository
    {
        public UsuarioRepository(RedeSocialContext db) : base(db) { }

        public Task<Usuario> AtualizaUsuarioAsync(Usuario usuario)
        {
            throw new NotImplementedException();
        }

        public async Task<Usuario> BuscaUsuarioAsync(string? nome, Guid? id)
        {
            var usuario = await _db.Usuario.FirstOrDefaultAsync(x => x.id == id);

            return usuario;
        }

        public Task<bool> DeletaUsuarioAsync(Guid id)
        {
            throw new NotImplementedException();
        }
    }
}
