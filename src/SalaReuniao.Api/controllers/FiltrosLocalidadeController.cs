using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Domain.Repositories;

[ApiController]
[Route("api/[controller]")]
public class FiltrosLocalidadeController : ControllerBase
{
    private readonly ObterFiltrosLocalidadeHandler _obterFiltrosLocalidadeHandler;

    public FiltrosLocalidadeController(ObterFiltrosLocalidadeHandler obterFiltrosLocalidadeHandler)
    {
        _obterFiltrosLocalidadeHandler = obterFiltrosLocalidadeHandler;
    }

    [HttpGet("filtros-localidade")]
    public async Task<IActionResult> ObterFiltrosLocalidade([FromQuery] ListarLocalidadesFilter filter)
    {
        var filtros = await _obterFiltrosLocalidadeHandler.HandleAsync(filter);
        return Ok(filtros);
    }
}
