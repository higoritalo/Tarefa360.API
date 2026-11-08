using Projeto360.Aplicacao.Contratos;
using Projeto360.Dominio.Entidades;

namespace Projeto360.Servicos.Servicos;

public class UsuarioServico : IUsuarioServico
{
    private readonly IUsuarioRepositorio _usuarioRepositorio;

    public UsuarioServico(IUsuarioRepositorio usuarioRepositorio)
    {
        _usuarioRepositorio = usuarioRepositorio;
    }

    public async Task<Usuario> CriarAsync(Usuario usuario)
    {
        return await _usuarioRepositorio.CriarAsync(usuario);
    }

    public async Task<List<Usuario>> ListarAsync()
    {
        return await _usuarioRepositorio.ListarAsync();
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        return await _usuarioRepositorio.ExcluirAsync(id);
    }

    public async Task<bool> AtualizarAsync(Usuario usuario)
    {
        return await _usuarioRepositorio.AtualizarAsync(usuario);
    }
}