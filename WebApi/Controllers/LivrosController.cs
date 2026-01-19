using Application;
using Application.Dtos;
using Microsoft.AspNetCore.Mvc;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace WebApi.Controllers;

[Route("api/[controller]")]
[ApiController]
public class LivrosController(ILivroAppService livroAppService) : ControllerBase
{
    [EndpointSummary("Cadastra um novo livro")]
    [EndpointDescription("Este endpoint valida os IDs de autores e assuntos antes de persistir o livro no SQL Server.")]
    [HttpPost]
    [ProducesResponseType(StatusCodes.Status201Created)]
    [ProducesResponseType(StatusCodes.Status400BadRequest)]
    [ProducesResponseType(StatusCodes.Status422UnprocessableEntity)]
    public async Task<IActionResult> Post([FromBody] LivroRequest request)
    {
        var codigo = await livroAppService.Cadastrar(request);

        return CreatedAtRoute("Obter", new { codigo }, request);
    }

    [HttpGet("{codigo:int}", Name = "Obter")]
    //[ProducesResponseType(typeof(LivroExibicaoDto), StatusCodes.Status200OK)]
    [ProducesResponseType(StatusCodes.Status404NotFound)]
    public async Task<IActionResult> Get(int codigo)
    {
        var livro = await livroAppService.ObterPorCodigo(codigo);
        if (livro == null) return NotFound();

        return Ok(livro);
    }

    [HttpGet]
    public async Task<IActionResult> Get()
    {
        return Ok(await livroAppService.ObterTodos());
    }

    [HttpGet("relatorio")]
    public async Task<IActionResult> Relatorio()
    {
        return Ok(await livroAppService.ObterRelatorio());
    }

    [HttpDelete("{codigo:int}")]
    public async Task<IActionResult> Delete(int codigo)
    {
        await livroAppService.Apagar(codigo);
        
        return NoContent();
    }
}
