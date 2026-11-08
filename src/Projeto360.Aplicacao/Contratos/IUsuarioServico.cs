using Projeto360.Dominio.Entidades;

namespace Projeto360.Aplicacao.Contratos;

public interface IUsuarioServico
{
    Task<Usuario> CriarAsync(Usuario usuario);
    Task<List<Usuario>> ListarAsync();
    Task<bool> ExcluirAsync(int id);
    Task<bool> AtualizarAsync(Usuario usuario);
}