using Microsoft.EntityFrameworkCore;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Dominio.Entidades;
using Projeto360.Repositorio.Dados;

namespace Projeto360.Repositorio.Repositorios;

public class ProjetoRepositorio : IProjetoRepositorio
{
    private readonly Projeto360Contexto _contexto;

    public ProjetoRepositorio(Projeto360Contexto contexto)
    {
        _contexto = contexto;
    }

    // Listar projetos
    public async Task<IEnumerable<Projeto>> ListarAsync()
    {
        return await _contexto.Projetos
            .AsNoTracking()
            .OrderBy(p => p.Id)
            .ToListAsync();
    }

    // Adicionar projeto
    public async Task AdicionarAsync(Projeto projeto)
    {
        await _contexto.Projetos.AddAsync(projeto);
        await _contexto.SaveChangesAsync();
    }

    // Buscar projeto por ID
    public async Task<Projeto?> ObterPorIdAsync(int id)
    {
        return await _contexto.Projetos
            .FirstOrDefaultAsync(p => p.Id == id);
    }

    // Atualizar projeto
    public async Task AtualizarAsync(Projeto projeto)
    {
        _contexto.Projetos.Update(projeto);
        await _contexto.SaveChangesAsync();
    }

    // Excluir projeto
    public async Task ExcluirAsync(Projeto projeto)
    {
        _contexto.Projetos.Remove(projeto);
        await _contexto.SaveChangesAsync();
    }
}