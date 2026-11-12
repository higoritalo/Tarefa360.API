using Projeto360.Aplicacao.Contratos;
using Projeto360.Aplicacao.DTOs;
using Projeto360.Dominio.Entidades;

namespace Projeto360.Servicos.Servicos;

public class ProjetoServico : IProjetoServico
{
    private readonly IProjetoRepositorio _projetoRepositorio;

    public ProjetoServico(IProjetoRepositorio projetoRepositorio)
    {
        _projetoRepositorio = projetoRepositorio;
    }

    // Listar projetos
    public async Task<IEnumerable<ProjetoRespostaDto>> ListarAsync()
    {
        var projetos = await _projetoRepositorio.ListarAsync();

        return projetos.Select(Mapear);
    }

    // Criar projeto
    public async Task<ProjetoRespostaDto> CriarAsync(CriarProjetoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do projeto é obrigatório.");

        var projeto = new Projeto
        {
            Nome = dto.Nome.Trim(),
            Descricao = dto.Descricao?.Trim()
        };

        await _projetoRepositorio.AdicionarAsync(projeto);

        return Mapear(projeto);
    }

    // Obter projeto por ID
    public async Task<ProjetoRespostaDto?> ObterPorIdAsync(int id)
    {
        var projeto = await _projetoRepositorio.ObterPorIdAsync(id);

        if (projeto == null)
            return null;

        return Mapear(projeto);
    }

    // Editar projeto
    public async Task<ProjetoRespostaDto?> EditarAsync(int id, CriarProjetoDto dto)
    {
        if (string.IsNullOrWhiteSpace(dto.Nome))
            throw new ArgumentException("O nome do projeto é obrigatório.");

        var projeto = await _projetoRepositorio.ObterPorIdAsync(id);

        if (projeto == null)
            return null;

        projeto.Nome = dto.Nome.Trim();
        projeto.Descricao = dto.Descricao?.Trim();

        await _projetoRepositorio.AtualizarAsync(projeto);

        return Mapear(projeto);
    }

    // Excluir projeto
    public async Task<bool> ExcluirAsync(int id)
    {
        var projeto = await _projetoRepositorio.ObterPorIdAsync(id);

        if (projeto == null)
            return false;

        await _projetoRepositorio.ExcluirAsync(projeto);

        return true;
    }

    // Mapear entidade para DTO
    private static ProjetoRespostaDto Mapear(Projeto projeto)
    {
        return new ProjetoRespostaDto
        {
            Id = projeto.Id,
            Nome = projeto.Nome,
            Descricao = projeto.Descricao
        };
    }
}