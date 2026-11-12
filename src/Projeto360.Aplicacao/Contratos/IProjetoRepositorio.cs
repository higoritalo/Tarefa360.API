using Projeto360.Dominio.Entidades;

namespace Projeto360.Aplicacao.Contratos;

public interface IProjetoRepositorio
{
    Task<IEnumerable<Projeto>> ListarAsync();

    Task AdicionarAsync(Projeto projeto);

    Task<Projeto?> ObterPorIdAsync(int id);

    Task AtualizarAsync(Projeto projeto);

    Task ExcluirAsync(Projeto projeto);
}