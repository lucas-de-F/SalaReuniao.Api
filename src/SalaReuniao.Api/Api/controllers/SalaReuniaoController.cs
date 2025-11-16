using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Core.Queries;

[ApiController]
[Route("api/[controller]")]
public class SalaDeReuniaoController : ControllerBase
{
    private readonly CriarSalaReuniaoHandler _handler;
    private readonly ListarSalasReuniaoHandler _listarHandler;
    private readonly ObterSalaReuniaoHandler _obterSalaReuniaoHandler;
    private readonly AtualizarSalaReuniaoHandler _atualizarHandler;
    private readonly RemoverSalaDeReuniaoHandler _removerHandler;

    public SalaDeReuniaoController(CriarSalaReuniaoHandler criarSalaReuniaoHandler, ListarSalasReuniaoHandler listarHandler, ObterSalaReuniaoHandler obterSalaReuniaoHandler, AtualizarSalaReuniaoHandler atualizarSalaReuniaoHandler, RemoverSalaDeReuniaoHandler removerSalaDeReuniaoHandler)
    {
        _handler = criarSalaReuniaoHandler;
        _listarHandler = listarHandler;
        _obterSalaReuniaoHandler = obterSalaReuniaoHandler;
        _atualizarHandler = atualizarSalaReuniaoHandler;
        _removerHandler = removerSalaDeReuniaoHandler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarSalaReuniaoCommand command)
    {
        var sala = await _handler.HandleAsync(command);
        return Ok(sala);
    }
    [HttpGet]
    public async Task<IActionResult> Listar([FromQuery] ListarSalasDeReuniaoFilter filter)
    {
        var salas = await _listarHandler.HandleAsync(filter);
        return Ok(salas);
    }
    [HttpGet("{id}")]
    public async Task<IActionResult> ObterSalaReuniao([FromRoute] Guid id)
    {
        var salas = await _obterSalaReuniaoHandler.HandleAsync(id);
        return Ok(salas);
    }
    [HttpPut("{idProprietario}")]
    public async Task<IActionResult> Atualizar([FromRoute] Guid idProprietario, [FromBody] AtualizarSalaReuniaoCommand command)
    {
        var salaAtualizada = await _atualizarHandler.HandleAsync(command, idProprietario);
        return Ok(salaAtualizada);
    }
    [HttpDelete("{id}")]
    public async Task<IActionResult> Remover(Guid id)
    {
        await _removerHandler.HandleAsync(id);
        return NoContent();
    }
}
