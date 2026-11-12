using Microsoft.AspNetCore.Mvc;
using Projeto360.Aplicacao.Contratos;
using Projeto360.Aplicacao.DTOs;

namespace Projeto360.Api.Controllers;

[ApiController]
[Route("api/[controller]")]
public class ProjetoController : ControllerBase
{
    private readonly IProjetoServico _servico;

    public ProjetoController(IProjetoServico servico)
    {
        _servico = servico;
    }

    // GET: api/projeto
    [HttpGet]
    public async Task<IActionResult> Listar()
    {
        return Ok(await _servico.ListarAsync());
    }

    // GET: api/projeto/1
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterPorId(int id)
    {
        var projeto = await _servico.ObterPorIdAsync(id);

        if (projeto == null)
            return NotFound();

        return Ok(projeto);
    }

    // POST: api/projeto
    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarProjetoDto dto)
    {
        try
        {
            var projeto = await _servico.CriarAsync(dto);

            return CreatedAtAction(nameof(ObterPorId), new { id = projeto.Id }, projeto);
        }
        catch (ArgumentException ex)
        {
            return BadRequest(new { mensagem = ex.Message });
        }
    }

    // PUT: api/projeto/1
    [HttpPut("{id}")]
    public async Task<IActionResult> Editar(int id, [FromBody] CriarProjetoDto dto)
    {
        var projeto = await _servico.EditarAsync(id, dto);

        if (projeto == null)
            return NotFound();

        return Ok(projeto);
    }

    // DELETE: api/projeto/1
    [HttpDelete("{id}")]
    public async Task<IActionResult> Excluir(int id)
    {
        var excluido = await _servico.ExcluirAsync(id);

        if (!excluido)
            return NotFound();

        return NoContent();
    }
}