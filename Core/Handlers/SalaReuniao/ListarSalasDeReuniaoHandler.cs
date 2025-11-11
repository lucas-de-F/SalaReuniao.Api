using AutoMapper;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;

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
        
        public async Task<ICollection<SalaDeReuniao>> HandleAsync(ListarSalasDeReuniaoQuery query)
        {
            var salaReuniaoEntity = await _salaReuniaoRepository.ObterTodasAsync(query.IdProprietario, query.Nome);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");

            var salasDeReuniao = mapper.Map<ICollection<SalaDeReuniao>>(salaReuniaoEntity);

            return salasDeReuniao;
        }
    }
}