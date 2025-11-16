using AutoMapper;
using Microsoft.AspNetCore.RateLimiting;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core
{
    public class ReservarSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IReservaSalaDeReuniaoRepository _reservaSalaDeReuniaoRepository;
        private readonly IMapper mapper;

        public ReservarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IReservaSalaDeReuniaoRepository reservaSalaDeReuniaoRepository, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            this.mapper = mapper;
            _reservaSalaDeReuniaoRepository = reservaSalaDeReuniaoRepository;
        }
        
        public async Task HandleAsync(ReservaSalaReuniaoCommand command)
        {
            var salaReuniaoEntity = await _salaReuniaoRepository.ObterPorIdAsync(command.IdSala);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");
            
            var salaReuniao = mapper.Map<SalaDeReuniao>(salaReuniaoEntity);
            salaReuniao.DisponibilidadeSemanal = DisponibilidadeSemanal.FromEntities(salaReuniaoEntity.Disponibilidades);
            
            if (!salaReuniao.AgendaDisponivel(command.Data, command.Inicio, command.Fim))
                throw new DomainException("A sala de reunião não está disponível no horário solicitado.");

            var reuniao = salaReuniao.AgendaReuniao(command.IdCliente, command.Data, command.Inicio, command.Fim);

            var salaReuniaoEntityAtualizada = mapper.Map<ReuniaoAgendadaEntity>(reuniao);
            await _reservaSalaDeReuniaoRepository.ReservarSalaAsync(salaReuniaoEntityAtualizada);
            await _salaReuniaoRepository.SalvarAlteracoesAsync();
        }
    }
}