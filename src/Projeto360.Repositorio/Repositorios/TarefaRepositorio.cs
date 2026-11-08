using Microsoft.EntityFrameworkCore;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Dominio.Entidades;
using Projeto360.Repositorio.Dados;

namespace Projeto360.Repositorio.Repositorios;

public class TarefaRepositorio : ITarefaRepositorio
{
    private readonly Projeto360Contexto _contexto;

    public TarefaRepositorio(Projeto360Contexto contexto)
    {
        _contexto = contexto;
    }

    public Task<List<Tarefa>> ListarAsync()
    {
        return _contexto.Tarefas.AsNoTracking()
            .OrderBy(t => t.Id)
            .ToListAsync();
    }

    public Task<Tarefa?> BuscarPorIdAsync(int id)
    {
        return _contexto.Tarefas.FirstOrDefaultAsync(t => t.Id == id);
    }

    public async Task AdicionarAsync(Tarefa tarefa)
    {
        await _contexto.Tarefas.AddAsync(tarefa);
        await _contexto.SaveChangesAsync();
    }

    public async Task AtualizarAsync(Tarefa tarefa)
    {
        _contexto.Tarefas.Update(tarefa);
        await _contexto.SaveChangesAsync();
    }

    public async Task RemoverAsync(Tarefa tarefa)
    {
        _contexto.Tarefas.Remove(tarefa);
        await _contexto.SaveChangesAsync();
    }
}
