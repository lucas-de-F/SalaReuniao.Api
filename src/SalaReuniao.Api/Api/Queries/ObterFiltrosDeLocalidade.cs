using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Core
{
    public class ObterFiltrosLocalidadeHandler
    {
        private readonly ILocalidadesSalaReuniaoRepository _localidadesSalaReuniaoRepository;
        private readonly IMapper mapper;

        public ObterFiltrosLocalidadeHandler(ILocalidadesSalaReuniaoRepository localidadesSalaReuniaoRepository, IMapper mapper)
        {
            _localidadesSalaReuniaoRepository = localidadesSalaReuniaoRepository;
            this.mapper = mapper;
        }
        
        public async Task<PagedResult<LocalidadeResult>> HandleAsync(ListarLocalidadesFilter query)
        {
            FilterLocalidade filter = mapper.Map<FilterLocalidade>(query);
            var resultado = await _localidadesSalaReuniaoRepository.ObterFiltrosLocalidade(
                filter
            );

            return resultado;
        }
    }
}