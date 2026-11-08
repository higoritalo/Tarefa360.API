using Microsoft.AspNetCore.Mvc;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Aplicacao.DTOs;

namespace Projeto360.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class TarefaController : ControllerBase
{
    private readonly ITarefaServico _servico;

    public TarefaController(ITarefaServico servico)
    {
        _servico = servico;
    }

    // GET: api/tarefa
    [HttpGet]
    [ProducesResponseType(typeof(List<TarefaRespostaDto>), StatusCodes.Status200OK)]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _servico.ListarAsync());
    }

    // GET: api/tarefa/1
    [HttpGet("{id:int}")]
    public async Task<IActionResult> BuscarPorId(int id)
    {
        var tarefa = await _servico.BuscarPorIdAsync(id);
        return tarefa is null ? NotFound(new { mensagem = "Tarefa não encontrada." }) : Ok(tarefa);
    }

    // POST: api/tarefa
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarTarefaDto dto)
    {
        try
        {
            var tarefa = await _servico.CriarAsync(dto);
            return CreatedAtAction(nameof(BuscarPorId), new { id = tarefa.Id }, tarefa);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    // PUT: api/tarefa/1
    [HttpPut("{id:int}")]
    public async Task<IActionResult> Atualizar(int id, [FromBody] AtualizarTarefaDto dto)
    {
        try
        {
            var tarefa = await _servico.AtualizarAsync(id, dto);
            return tarefa is null ? NotFound(new { mensagem = "Tarefa não encontrada." }) : Ok(tarefa);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    // PATCH: api/tarefa/1/concluir
    [HttpPatch("{id:int}/concluir")]
    public async Task<IActionResult> Concluir(int id)
    {
        var tarefa = await _servico.ConcluirAsync(id);
        return tarefa is null ? NotFound(new { mensagem = "Tarefa não encontrada." }) : Ok(tarefa);
    }

    // DELETE: api/tarefa/1
    [HttpDelete("{id:int}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluida = await _servico.ExcluirAsync(id);
        return excluida ? NoContent() : NotFound(new { mensagem = "Tarefa não encontrada." });
    }
}
