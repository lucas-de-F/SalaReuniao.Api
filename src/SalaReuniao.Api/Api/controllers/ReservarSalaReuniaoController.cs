using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Core.Queries;

[ApiController]
[Route("api/[controller]")]
public class ReservasController : ControllerBase
{
    private readonly ReservarSalaReuniaoHandler _reservarHandler;
    private readonly CancelarReservaSalaReuniaoHandler _cancelarHandler;
    private readonly ListarAgendamentosHandler _agendamentosHandler;
    public ReservasController(ReservarSalaReuniaoHandler reservarSalaReuniaoHandler, CancelarReservaSalaReuniaoHandler cancelarReservaSalaReuniaoHandler, ListarAgendamentosHandler agendamentosHandler)
    {
        _reservarHandler = reservarSalaReuniaoHandler;
        _cancelarHandler = cancelarReservaSalaReuniaoHandler;
        _agendamentosHandler = agendamentosHandler;
    }

    [Authorize]
    [HttpPost("Reservar")]
    public async Task<IActionResult> ReservarSala([FromBody] ReservaSalaReuniaoCommand command)
    {
        await _reservarHandler.HandleAsync(command);
        return Ok();
    }
    [Authorize]
    [HttpPost("Cancelar/{id}")]
    public async Task<IActionResult> CancelarReservaSala([FromRoute] Guid id)
    {
        await _cancelarHandler.HandleAsync(new CancelarReservaSalaReuniaoCommand { IdReserva = id });
        return Ok();
    }
    [Authorize]
    [HttpGet]
    public async Task<IActionResult> ListarAgendamentos([FromQuery] ListarAgendamentosFilter filter)
    {
        var result = await _agendamentosHandler.HandleAsync(filter);
        return Ok(result);
    }
}
