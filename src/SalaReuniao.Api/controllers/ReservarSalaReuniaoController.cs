using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Domain.Repositories;

[ApiController]
[Route("api/[controller]")]
public class ReservarSalaReuniaoController : ControllerBase
{
    private readonly ReservarSalaReuniaoHandler _reservarHandler;
    private readonly CancelarReservaSalaReuniaoHandler _cancelarHandler;
    public ReservarSalaReuniaoController(ReservarSalaReuniaoHandler reservarSalaReuniaoHandler, CancelarReservaSalaReuniaoHandler cancelarReservaSalaReuniaoHandler)
    {
        _reservarHandler = reservarSalaReuniaoHandler;
        _cancelarHandler = cancelarReservaSalaReuniaoHandler;
    }

    [HttpPost]
    public async Task<IActionResult> ReservarSala([FromBody] ReservaSalaReuniaoCommand command)
    {
        await _reservarHandler.HandleAsync(command);
        return Ok();
    }

    [HttpPost("Cancelar/{id}")]
    public async Task<IActionResult> CancelarReservaSala([FromRoute] Guid id)
    {
        await _cancelarHandler.HandleAsync(new CancelarReservaSalaReuniaoCommand { IdReserva = id });
        return Ok();
    }
}
