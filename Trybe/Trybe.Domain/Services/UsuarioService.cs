using Trybe.Domain.Entidades;
using Trybe.Domain.Interfaces.Aplicacao;
using Trybe.Domain.Interfaces.Repositorio;

namespace Trybe.Domain.Services
{
    public class UsuarioService : IUsuarioService
    {
        private readonly IUsuarioRepository _usuarioRepository;

        public UsuarioService(IUsuarioRepository usuarioRepository)
        {
            _usuarioRepository = usuarioRepository;
        }

        public async Task<bool> AdicionaUsuarioAsync(Usuario usuario)
        {
            var retornoUsuario = await _usuarioRepository.AdicionarAsync(usuario);

            return retornoUsuario != null;
        }

        public async Task<Usuario> AtualizaUsuarioAsync(Usuario usuario)
        {
            var usuarioAntigo = await _usuarioRepository.ObterPorIdAsync(usuario.id);
            usuarioAntigo.Nome = usuario.Nome;

            var usuarioNovo = await _usuarioRepository.AtualizarAsync(usuarioAntigo);

            return usuarioNovo;
        }

        public async Task<Usuario> BuscaUsuarioPorIdAsync(Guid id)
        {
            var usuario = await _usuarioRepository.BuscaUsuarioAsync(null, id);

            if (usuario == null)
                return new Usuario();

            return usuario;
        }

        public async Task<Usuario> BuscaUsuarioPorNomeAsync(string nome)
        {
            if (string.IsNullOrEmpty(nome))
                return new Usuario();

            var usuario = await _usuarioRepository.BuscaUsuarioAsync(nome, null);

            if (usuario == null)
                return new Usuario();

            return usuario;
        }

        public async Task DeletaUsuarioAsync(Guid id) =>
            await _usuarioRepository.DeletarAsync(id);

    }
}
