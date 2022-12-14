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

    }
}
