using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Dominio.Entidades;

namespace Projeto360.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class UsuarioController : ControllerBase
{
    private readonly IUsuarioServico _usuarioServico;

    public UsuarioController(IUsuarioServico usuarioServico)
    {
        _usuarioServico = usuarioServico;
    }

    // Lista os tipos de usuários.
    [HttpGet("ListarTiposUsuario")]
    public IActionResult ListarTiposUsuario()
    {
        var tipos = new[]
        {
            new { Id = 1, Descricao = "Administrador" },
            new { Id = 2, Descricao = "Gestor" },
            new { Id = 3, Descricao = "Desenvolvedor" }
        };

        return Ok(tipos);
    }

    // Realiza o cadastro de usuários.
    [HttpPost("Criar")]
    public async Task<IActionResult> Criar([FromBody] UsuarioCriarDto dados)
    {
        if (string.IsNullOrWhiteSpace(dados.Nome) ||
            string.IsNullOrWhiteSpace(dados.Email) ||
            string.IsNullOrWhiteSpace(dados.Senha))
        {
            return BadRequest("Preencha todos os campos.");
        }

        if (dados.TipoUsuario < 1 || dados.TipoUsuario > 3)
        {
            return BadRequest("Tipo de usuário inválido.");
        }

        var usuario = new Usuario
        {
            Nome = dados.Nome,
            Email = dados.Email,
            TipoUsuario = dados.TipoUsuario
        };

        // Criptografa a senha antes de salvar.
        var passwordHasher = new PasswordHasher<Usuario>();

        usuario.SenhaHash = passwordHasher.HashPassword(
            usuario,
            dados.Senha
        );

        var usuarioCriado = await _usuarioServico.CriarAsync(usuario);

        return Ok(new
        {
            usuarioCriado.Id,
            usuarioCriado.Nome,
            usuarioCriado.Email,
            usuarioCriado.TipoUsuario
        });
    }

    // Lista os usuários cadastrados.
    [HttpGet("Listar")]
    public async Task<IActionResult> Listar()
    {
        var usuarios = await _usuarioServico.ListarAsync();

        var resultado = usuarios.Select(usuario => new
        {
            usuario.Id,
            usuario.Nome,
            usuario.Email,
            usuario.TipoUsuario
        });

        return Ok(resultado);
    }

    // Exclui um usuário pelo ID.
    [HttpDelete("Excluir/{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await _usuarioServico.ExcluirAsync(id);

        if (!excluido)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok("Usuário excluído com sucesso.");
    }

    // Atualiza os dados de um usuário.
    [HttpPut("Atualizar")]
    public async Task<IActionResult> Atualizar([FromBody] UsuarioAtualizarDto dados)
    {
        if (string.IsNullOrWhiteSpace(dados.Nome) ||
            string.IsNullOrWhiteSpace(dados.Email))
        {
            return BadRequest("Preencha todos os campos.");
        }

        if (dados.TipoUsuario < 1 || dados.TipoUsuario > 3)
        {
            return BadRequest("Tipo de usuário inválido.");
        }

        var usuario = new Usuario
        {
            Id = dados.Id,
            Nome = dados.Nome,
            Email = dados.Email,
            TipoUsuario = dados.TipoUsuario
        };

        var atualizado = await _usuarioServico.AtualizarAsync(usuario);

        if (!atualizado)
        {
            return NotFound("Usuário não encontrado.");
        }

        return Ok("Usuário atualizado com sucesso.");
    }
}

// Dados recebidos do formulário React.
public class UsuarioCriarDto
{
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public string Senha { get; set; } = string.Empty;
    public int TipoUsuario { get; set; }
}

// Dados recebidos para atualizar um usuário.
public class UsuarioAtualizarDto
{
    public int Id { get; set; }
    public string Nome { get; set; } = string.Empty;
    public string Email { get; set; } = string.Empty;
    public int TipoUsuario { get; set; }
}