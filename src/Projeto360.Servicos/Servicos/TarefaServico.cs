using Projeto360.Aplicacao.Contratos;
using Projeto360.Aplicacao.DTOs;
using Projeto360.Dominio.Entidades;

namespace Projeto360.Servicos.Servicos;

public class TarefaServico : ITarefaServico
{
    private readonly ITarefaRepositorio _repositorio;

    public TarefaServico(ITarefaRepositorio repositorio)
    {
        _repositorio = repositorio;
    }

    public async Task<List<TarefaRespostaDto>> ListarAsync()
    {
        var tarefas = await _repositorio.ListarAsync();
        return tarefas.Select(Mapear).ToList();
    }

    public async Task<TarefaRespostaDto?> BuscarPorIdAsync(int id)
    {
        if (id <= 0) return null;

        var tarefa = await _repositorio.BuscarPorIdAsync(id);
        return tarefa is null ? null : Mapear(tarefa);
    }

    public async Task<TarefaRespostaDto> CriarAsync(CriarTarefaDto dto)
    {
        var tarefa = new Tarefa(dto.Titulo);
        await _repositorio.AdicionarAsync(tarefa);
        return Mapear(tarefa);
    }

    public async Task<TarefaRespostaDto?> AtualizarAsync(int id, AtualizarTarefaDto dto)
    {
        var tarefa = await _repositorio.BuscarPorIdAsync(id);
        if (tarefa is null) return null;

        tarefa.AlterarTitulo(dto.Titulo);
        if (dto.Concluida)
            tarefa.MarcarComoConcluida();
        else
            tarefa.Reabrir();

        await _repositorio.AtualizarAsync(tarefa);
        return Mapear(tarefa);
    }

    public async Task<TarefaRespostaDto?> ConcluirAsync(int id)
    {
        var tarefa = await _repositorio.BuscarPorIdAsync(id);
        if (tarefa is null) return null;

        tarefa.MarcarComoConcluida();
        await _repositorio.AtualizarAsync(tarefa);
        return Mapear(tarefa);
    }

    public async Task<bool> ExcluirAsync(int id)
    {
        var tarefa = await _repositorio.BuscarPorIdAsync(id);
        if (tarefa is null) return false;

        await _repositorio.RemoverAsync(tarefa);
        return true;
    }

    private static TarefaRespostaDto Mapear(Tarefa tarefa)
    {
        return new TarefaRespostaDto
        {
            Id = tarefa.Id,
            Titulo = tarefa.Titulo,
            Concluida = tarefa.Concluida
        };
    }
}
