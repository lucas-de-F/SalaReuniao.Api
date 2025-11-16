using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Core
{
    public class ObterSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IMapper mapper;

        public ObterSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            this.mapper = mapper;
        }
        
        public async Task<SalaDeReuniaoDetalhadaResult> HandleAsync(Guid id)
        {
            var resultado = await _salaReuniaoRepository.ObterPorIdAsync(
                id
            );

            return resultado == null 
                ? throw new DomainException("Sala de reunião não encontrada.") 
                : mapper.Map<SalaDeReuniaoDetalhadaResult>(resultado);
        }
    }
}