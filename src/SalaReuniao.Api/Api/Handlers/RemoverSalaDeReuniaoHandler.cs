using AutoMapper;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Core
{
    public class RemoverSalaDeReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IMapper mapper;

        public RemoverSalaDeReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            this.mapper = mapper;
        }
        
        public async Task HandleAsync(Guid Id, Guid ResponsavelId)
        {
            var salaReuniaoEntity = await _salaReuniaoRepository.ObterPorIdAsync(Id);

            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");
                
            if (salaReuniaoEntity.IdResponsavel != ResponsavelId)
                throw new DomainException("Você não tem permissão para remover esta sala de reunião.");
                
            await _salaReuniaoRepository.RemoverAsync(salaReuniaoEntity);
            await _salaReuniaoRepository.SalvarAlteracoesAsync();
        }
    }
}