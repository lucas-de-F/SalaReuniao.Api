using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Infrastructure.Entities;
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
            var resultado = await _salaReuniaoRepository.ObterTodasAsync(
                query.Id,
                query.IdProprietario,
                query.Nome,
                query.Page,
                query.PageSize
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