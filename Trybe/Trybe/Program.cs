using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Options;
using Microsoft.OpenApi.Models;
using System.Text.Json.Serialization;
using Trybe.Domain.Context;
using Trybe.Domain.Helpers;
using Trybe.Domain.Interfaces.Aplicacao;
using Trybe.Domain.Interfaces.Repositorio;
using Trybe.Domain.Services;
using Trybe.Repository.Repositories;

var builder = WebApplication.CreateBuilder(args);

builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.DefaultIgnoreCondition = JsonIgnoreCondition.WhenWritingNull;
});

builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new OpenApiInfo { Title = "APIContagem", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description =
            "JWT Authorization Header - utilizado com Bearer Authentication.\r\n\r\n" +
            "Digite 'Bearer' [espaço] e então seu token no campo abaixo.\r\n\r\n" +
            "Exemplo (informar sem as aspas): 'Bearer 12345abcdef'",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer",
        BearerFormat = "JWT",
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

var tokenConfigurations = new TokenConfigurations();
new ConfigureFromConfigurationOptions<TokenConfigurations>(
    builder.Configuration.GetSection("TokenConfigurations"))
        .Configure(tokenConfigurations);

// Aciona a extensão que irá configurar o uso de
// autenticação e autorização via tokens
builder.Services.AddJwtSecurity(tokenConfigurations);

var connectionString = builder.Configuration.GetConnectionString("DefaultConnection");
builder.Services.AddDbContext<RedeSocialContext>(x => x.UseSqlServer(connectionString));

// Acionar caso seja necessário criar usuários para testes
builder.Services.AddScoped<InicializadorAutenticacao>();

builder.Services.AddScoped<IUsuarioService, UsuarioService>();
builder.Services.AddScoped<IUsuarioRepository, UsuarioRepository>();


var app = builder.Build();


app.UseSwagger();
app.UseSwaggerUI();

app.UseHttpsRedirection();

// Criação de estruturas, usuários e permissões
// na base do ASP.NET Identity Core (caso ainda não
// existam)
//var identityInitializer = app.Services.GetRequiredService<IdentityInitializer>();
using var scope = app.Services.CreateScope();
scope.ServiceProvider.GetRequiredService<InicializadorAutenticacao>().Initialize();

app.UseAuthorization();

app.MapControllers();

app.Run();