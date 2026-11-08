using Microsoft.EntityFrameworkCore;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Repositorio.Dados;
using Projeto360.Repositorio.Repositorios;
using Projeto360.Servicos.Servicos;

var builder = WebApplication.CreateBuilder(args);

// Configuração dos serviços da API.
builder.Services.AddControllers();
builder.Services.AddEndpointsApiExplorer();
builder.Services.AddSwaggerGen();

// Configuração do CORS.
builder.Services.AddCors(options =>
{
    options.AddDefaultPolicy(cors =>
    {
        cors.WithOrigins(
            "http://localhost:3000",
            "https://localhost:3000"
        )
        .SetIsOriginAllowedToAllowWildcardSubdomains()
        .AllowAnyHeader()
        .AllowAnyMethod();
    });
});

// Configuração do banco de dados SQLite.
var caminhoBanco = Path.Combine(
    builder.Environment.ContentRootPath,
    "projeto360.db"
);

builder.Services.AddDbContext<Projeto360Contexto>(opcoes =>
    opcoes.UseSqlite($"Data Source={caminhoBanco}")
);

// Injeção de dependência: interfaces e implementações.
builder.Services.AddScoped<ITarefaRepositorio, TarefaRepositorio>();
builder.Services.AddScoped<ITarefaServico, TarefaServico>();

// Serviços de usuários.
builder.Services.AddScoped<IUsuarioRepositorio, UsuarioRepositorio>();
builder.Services.AddScoped<IUsuarioServico, UsuarioServico>();

var app = builder.Build();

// Cria o banco e as tabelas automaticamente.
using (var escopo = app.Services.CreateScope())
{
    var contexto = escopo.ServiceProvider
        .GetRequiredService<Projeto360Contexto>();

    contexto.Database.EnsureCreated();

    contexto.Database.ExecuteSqlRaw(@"
        CREATE TABLE IF NOT EXISTS Usuarios (
            Id INTEGER NOT NULL PRIMARY KEY AUTOINCREMENT,
            Nome TEXT NOT NULL,
            Email TEXT NOT NULL,
            SenhaHash TEXT NOT NULL,
            TipoUsuario INTEGER NOT NULL
        );
    ");
}

// Configuração do CORS.
app.UseCors();

if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Mapeamento dos controllers.
app.MapControllers();

app.Run();
