using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;

public class AtualizarSalaReuniaoHandler
{
    private readonly SalaDeReuniaoAppService _salaService;
    private readonly DisponibilidadeAppService _dispService;

    public AtualizarSalaReuniaoHandler(
        SalaDeReuniaoAppService salaService,
        DisponibilidadeAppService dispService)
    {
        _salaService = salaService;
        _dispService = dispService;
    }

    public async Task<SalaDeReuniao> HandleAsync(
        AtualizarSalaReuniaoCommand command, Guid idProprietario)
    {
        var sala = await _salaService.AtualizarDadosDaSalaAsync(command, idProprietario);
        await _dispService.AtualizarDisponibilidadesAsync(sala);

        return sala;
    }
}
