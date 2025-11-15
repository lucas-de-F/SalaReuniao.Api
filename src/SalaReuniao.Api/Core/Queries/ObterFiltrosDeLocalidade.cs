using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Core
{
    public class ObterFiltrosLocalidadeHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IMapper mapper;

        public ObterFiltrosLocalidadeHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            this.mapper = mapper;
        }
        
        public async Task<PagedResult<FiltrosLocalidadeResult>> HandleAsync(ListarLocalidadesFilter query)
        {
            FilterLocalidade filter = mapper.Map<FilterLocalidade>(query);
            var resultado = await _salaReuniaoRepository.ObterFiltrosLocalidade(
                filter
            );


            return resultado;
        }
    }
}