using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Core
{
    public class ListarSalasReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IMapper mapper;

        public ListarSalasReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            this.mapper = mapper;
        }
        
        public async Task<PagedResult<SalaDeReuniaoResult>> HandleAsync(ListarSalasDeReuniaoFilter query)
        {
            FilterSalaReuniao filter = mapper.Map<FilterSalaReuniao>(query);
            var resultado = await _salaReuniaoRepository.ObterTodasAsync(
                filter
            );

            var itensMapeados = mapper.Map<ICollection<SalaDeReuniaoResult>>(resultado.Items);

            return new PagedResult<SalaDeReuniaoResult>(
                itensMapeados,
                resultado.TotalItems,
                resultado.Page,
                resultado.PageSize
            );
        }
    }
}