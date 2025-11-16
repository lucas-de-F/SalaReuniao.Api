using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Queries;

[ApiController]
[Route("api/[controller]")]
public class LoginController : ControllerBase
{
    private readonly LoginUsuarioHandler _loginUsuarioHandler;

    public LoginController(LoginUsuarioHandler loginUsuarioHandler)
    {
        _loginUsuarioHandler = loginUsuarioHandler;
    }

    [HttpGet("{username}")]
    [AllowAnonymous]
    public async Task<IActionResult> ObterFiltrosLocalidade([FromRoute] string username)
    {
        var filtros = await _loginUsuarioHandler.HandleAsync(username);
        return Ok(filtros);
    }
}
