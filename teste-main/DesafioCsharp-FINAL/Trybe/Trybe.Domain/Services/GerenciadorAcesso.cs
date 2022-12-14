using System.IdentityModel.Tokens.Jwt;
using System.Security.Claims;
using System.Security.Principal;
using Microsoft.AspNetCore.Identity;
using Microsoft.IdentityModel.Tokens;
using Trybe.Domain.Configurations;
using Trybe.Domain.Entidades;
using Trybe.Domain.Interfaces.Repositorio;

namespace Trybe.Domain.Services
{
    public class GerenciadorAcesso
    {
        private UserManager<ApplicationUser> _userManager;
        private SignInManager<ApplicationUser> _signInManager;
        private SigningConfigurations _signingConfigurations;
        private TokenConfigurations _tokenConfigurations;
        private readonly IUsuarioRepository _usuarioRepository;


        public GerenciadorAcesso(
            UserManager<ApplicationUser> userManager,
            SignInManager<ApplicationUser> signInManager,
            SigningConfigurations signingConfigurations,
            TokenConfigurations tokenConfigurations,
            IUsuarioRepository usuarioRepository)
        {
            _userManager = userManager;
            _signInManager = signInManager;
            _signingConfigurations = signingConfigurations;
            _tokenConfigurations = tokenConfigurations;
            _usuarioRepository = usuarioRepository;
        }

        public async Task <bool> ValidateCredentials(User user)
        {
            bool credenciaisValidas = false;
            if (user is not null && !String.IsNullOrWhiteSpace(user.UserID))
            {
                // Verifica a existência do usuário nas tabelas do
                // ASP.NET Core Identity
                var userIdentity = await _usuarioRepository
                    .ObterAsync(x => x.Nome == user.UserID);
                if (userIdentity is not null)
                {
                    // Efetua o login com base no Id do usuário e sua senha
                    var resultadoLogin = await _usuarioRepository.ObterAsync(x => x.Nome == user.UserID && x.Senha == user.Password);
                    if(resultadoLogin.Any())
                        credenciaisValidas = true;
                }
            }

            return credenciaisValidas;
        }

        public Token GenerateToken(User user)
        {
            ClaimsIdentity identity = new(
                new GenericIdentity(user.UserID!, "Login"),
                new[] {
                        new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString("N")),
                        new Claim(JwtRegisteredClaimNames.UniqueName, user.UserID!)
                }
            );

            DateTime dataCriacao = DateTime.Now;
            DateTime dataExpiracao = dataCriacao +
                TimeSpan.FromSeconds(_tokenConfigurations.Seconds);

            var handler = new JwtSecurityTokenHandler();
            var securityToken = handler.CreateToken(new SecurityTokenDescriptor
            {
                Issuer = _tokenConfigurations.Issuer,
                Audience = _tokenConfigurations.Audience,
                SigningCredentials = _signingConfigurations.SigningCredentials,
                Subject = identity,
                NotBefore = dataCriacao,
                Expires = dataExpiracao
            });
            var token = handler.WriteToken(securityToken);

            return new ()
            {
                Authenticated = true,
                Created = dataCriacao.ToString("yyyy-MM-dd HH:mm:ss"),
                Expiration = dataExpiracao.ToString("yyyy-MM-dd HH:mm:ss"),
                AccessToken = token,
                Message = "OK"
            };
        }
    }
}
