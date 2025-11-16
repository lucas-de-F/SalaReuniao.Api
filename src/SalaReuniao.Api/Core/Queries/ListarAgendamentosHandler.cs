using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Core
{
    public class ListarAgendamentosHandler
    {
        private readonly IAgendamentosRepository _agendamentosRepository;
        private readonly IMapper mapper;

        public ListarAgendamentosHandler(IAgendamentosRepository agendamentosRepository, IMapper mapper)
        {
            _agendamentosRepository = agendamentosRepository;
            this.mapper = mapper;
        }
        
        public async Task<PagedResult<ReuniaoAgendadaSimpleResult>> HandleAsync(ListarAgendamentosFilter query)
        {
            FilterAgendamentos filter = mapper.Map<FilterAgendamentos>(query);
            var resultado = await _agendamentosRepository.ObterAgendamentosAsync(
                filter
            );

            var itensMapeados = mapper.Map<ICollection<ReuniaoAgendadaSimpleResult>>(resultado.Items);

            return new PagedResult<ReuniaoAgendadaSimpleResult>(
                itensMapeados,
                resultado.TotalItems,
                resultado.Page,
                resultado.PageSize
            );
        }
    }
}