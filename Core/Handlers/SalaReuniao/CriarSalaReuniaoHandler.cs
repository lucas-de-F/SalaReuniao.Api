using AutoMapper;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Core
{
    public class CriarSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _repository;
        private readonly IResponsavelRepository _responsavelRepo;
        private readonly IMapper mapper;

        public CriarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IResponsavelRepository responsavelRepo, IMapper mapper)
        {
            _repository = salaDeReuniaoRepository;
            _responsavelRepo = responsavelRepo;
            this.mapper = mapper;
        }
        
        public async Task<SalaDeReuniao> HandleAsync(CriarSalaReuniaoCommand command)
        {
            var responsavel = await _responsavelRepo.ObterResponsavelAsync(command.IdResponsavel);
            if (responsavel == null)
                throw new DomainException("Responsável não encontrado.");

            var sala = SalaDeReuniao.Criar(command);

            var salaEntity = mapper.Map<SalaDeReuniaoEntity>(sala);
            await _repository.AdicionarAsync(salaEntity);
            await _repository.SalvarAlteracoesAsync();

            return sala;
        }
    }

}