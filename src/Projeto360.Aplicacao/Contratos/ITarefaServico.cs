using Projeto360.Aplicacao.DTOs;

namespace Projeto360.Aplicacao.Contratos;

public interface ITarefaServico
{
    Task<List<TarefaRespostaDto>> ListarAsync();
    Task<TarefaRespostaDto?> BuscarPorIdAsync(int id);
    Task<TarefaRespostaDto> CriarAsync(CriarTarefaDto dto);
    Task<TarefaRespostaDto?> AtualizarAsync(int id, AtualizarTarefaDto dto);
    Task<TarefaRespostaDto?> ConcluirAsync(int id);
    Task<bool> ExcluirAsync(int id);
}
