using Microsoft.EntityFrameworkCore;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Dominio.Entidades;
using Projeto360.Repositorio.Dados;

namespace Projeto360.Repositorio.Repositorios;

public class UsuarioRepositorio : IUsuarioRepositorio
{
    private readonly Projeto360Contexto _contexto;

    public UsuarioRepositorio(Projeto360Contexto contexto)
    {
        _contexto = contexto;
    }

    public async Task<Usuario> CriarAsync(Usuario usuario)
    {
        _contexto.Usuarios.Add(usuario);

        await _contexto.SaveChangesAsync();

        return usuario;
    }

    public async Task<List<Usuario>> ListarAsync()
    {
        return await _contexto.Usuarios
            .AsNoTracking()
            .ToListAsync();
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var usuario = await _contexto.Usuarios.FindAsync(id);

        if (usuario == null)
        {
            return false;
        }

        _contexto.Usuarios.Remove(usuario);

        await _contexto.SaveChangesAsync();

        return true;
    }

    // Atualiza os dados do usuário.
    public async Task<bool> AtualizarAsync(Usuario usuario)
    {
        var usuarioExistente = await _contexto.Usuarios.FindAsync(usuario.Id);

        if (usuarioExistente == null)
        {
            return false;
        }

        usuarioExistente.Nome = usuario.Nome;
        usuarioExistente.Email = usuario.Email;
        usuarioExistente.TipoUsuario = usuario.TipoUsuario;

        await _contexto.SaveChangesAsync();

        return true;
    }
}