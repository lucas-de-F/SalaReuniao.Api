using AutoMapper;
using Microsoft.AspNetCore.RateLimiting;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Core
{
    public class CancelarReservaSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IReservaSalaDeReuniaoRepository _reservaSalaDeReuniaoRepository;
        private readonly IMapper mapper;

        public CancelarReservaSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IReservaSalaDeReuniaoRepository reservaSalaDeReuniaoRepository, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            this.mapper = mapper;
            _reservaSalaDeReuniaoRepository = reservaSalaDeReuniaoRepository;
        }
        
        public async Task HandleAsync(CancelarReservaSalaReuniaoCommand command)
        {
            var reservas = await _reservaSalaDeReuniaoRepository.ObterTodasAsync(new Domain.Filters.FilterReuniaoReservada
            {
                Id = command.IdReserva,
                IdCliente = command.UserId
            });

            if (reservas.TotalItems == 0)
                throw new DomainException("Reserva não encontrada.");

            var reserva = reservas.Items.First();
            
            var salaReuniaoEntity = await _salaReuniaoRepository.ObterPorIdAsync(reserva.IdSalaReuniao);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");
            
            var reuniaoAgendada = mapper.Map<ReuniaoAgendada>(reserva); 
            reuniaoAgendada.Cancelar();

            var salaReuniaoEntityAtualizada = mapper.Map<ReuniaoAgendadaEntity>(reuniaoAgendada);
            await _reservaSalaDeReuniaoRepository.AtualizarReservarSalaAsync(salaReuniaoEntityAtualizada);
            await _reservaSalaDeReuniaoRepository.SalvarAlteracoesAsync();
        }
    }
}