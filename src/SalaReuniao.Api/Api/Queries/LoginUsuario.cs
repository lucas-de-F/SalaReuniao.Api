using SalaReuniao.Domain.Repositories;

public class LoginUsuarioHandler
{
    private readonly IUsuarioRepository _usuarioRepository;
    private readonly JwtTokenService _tokenService;

    public LoginUsuarioHandler(
        IUsuarioRepository usuarioRepository,
        JwtTokenService tokenService)
    {
        _usuarioRepository = usuarioRepository;
        _tokenService = tokenService;
    }

    public async Task<object> HandleAsync(string nomeUsuario)
    {
        var usuario = await _usuarioRepository.ObterUsuarioAsync(nomeUsuario);

        if (usuario == null)
            throw new KeyNotFoundException("Usuário não encontrado.");

        return new { Token = _tokenService.GenerateToken(usuario.Id) };
    }
}
