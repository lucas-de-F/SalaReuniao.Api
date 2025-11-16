using AutoMapper;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.Services;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core
{
    public class CriarSalaReuniaoHandler
    {
        private readonly DisponibilidadeAppService _dispService;
        private readonly SalaDeReuniaoAppService _salaService;

        public CriarSalaReuniaoHandler(DisponibilidadeAppService dispService, SalaDeReuniaoAppService salaService)
        {
            _dispService = dispService;
            _salaService = salaService;
        }

        public async Task<SalaDeReuniao> HandleAsync(CriarSalaReuniaoCommand command)
        {
            var sala = await _salaService.CriarSalaDeReuniaoAsync(
                command
            );
            await _dispService.AtualizarDisponibilidadesAsync(sala);

            return sala;
        }
    }

}