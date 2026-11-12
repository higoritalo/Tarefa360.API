using Projeto360.Aplicacao.DTOs;

namespace Projeto360.Aplicacao.Contratos;

public interface IProjetoServico
{
    Task<IEnumerable<ProjetoRespostaDto>> ListarAsync();

    Task<ProjetoRespostaDto> CriarAsync(CriarProjetoDto dto);

    Task<ProjetoRespostaDto?> ObterPorIdAsync(int id);

    Task<ProjetoRespostaDto?> EditarAsync(int id, CriarProjetoDto dto);

    Task<bool> ExcluirAsync(int id);
}
