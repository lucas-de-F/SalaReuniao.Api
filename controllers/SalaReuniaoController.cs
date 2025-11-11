using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;

[ApiController]
[Route("api/[controller]")]
public class SalaDeReuniaoController : ControllerBase
{
    private readonly CriarSalaReuniaoHandler _handler;

    public SalaDeReuniaoController(CriarSalaReuniaoHandler handler)
    {
        _handler = handler;
    }

    [HttpPost]
    public async Task<IActionResult> Criar([FromBody] CriarSalaReuniaoCommand command)
    {
        var sala = await _handler.HandleAsync(command);
        return Ok(sala);
    }
}
