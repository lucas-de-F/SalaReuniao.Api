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
            var salaReuniaoEntity = await _salaReuniaoRepository.ObterPorIdAsync(command.IdSala);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");
            
            var reuniao = salaReuniaoEntity.ReunioesAgendadas.FirstOrDefault(r => r.Id == command.IdReserva);
            var reuniaoAgendada = mapper.Map<ReuniaoAgendada>(reuniao); 

            reuniaoAgendada.Cancelar();

            var salaReuniaoEntityAtualizada = mapper.Map<ReuniaoAgendadaEntity>(reuniaoAgendada);
            await _reservaSalaDeReuniaoRepository.ReservarSalaAsync(salaReuniaoEntityAtualizada);
            await _salaReuniaoRepository.SalvarAlteracoesAsync();
        }
    }
}