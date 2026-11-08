using Projeto360.Dominio.Entidades;

namespace Projeto360.Aplicacao.Contratos;

public interface ITarefaRepositorio
{
    Task<List<Tarefa>> ListarAsync();
    Task<Tarefa?> BuscarPorIdAsync(int id);
    Task AdicionarAsync(Tarefa tarefa);
    Task AtualizarAsync(Tarefa tarefa);
    Task RemoverAsync(Tarefa tarefa);
}
