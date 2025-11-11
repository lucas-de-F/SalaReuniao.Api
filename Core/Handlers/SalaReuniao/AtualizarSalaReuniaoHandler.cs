using AutoMapper;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Core
{
    public class AtualizarSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _salaReuniaoRepository;
        private readonly IResponsavelRepository _responsavelRepo;
        private readonly IMapper mapper;

        public AtualizarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IResponsavelRepository responsavelRepo, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            _responsavelRepo = responsavelRepo;
            this.mapper = mapper;
        }
        
        public async Task<SalaDeReuniao> HandleAsync(AtualizarSalaReuniaoCommand command, Guid IdProprietario)
        {
            if (command.IdResponsavel != IdProprietario)
                throw new DomainException("Você não tem permissão para atualizar esta sala de reunião.");
                
            var responsavel = await _responsavelRepo.ObterResponsavelAsync(command.IdResponsavel);
            if (responsavel == null)
                throw new DomainException("Responsável não encontrado.");

            var salaReuniaoEntity = await _salaReuniaoRepository.ObterPorIdAsync(command.Id);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");

            var salaReuniao = mapper.Map<SalaDeReuniao>(salaReuniaoEntity);
            var sala = salaReuniao.Atualizar(command);

            var salaEntity = mapper.Map<SalaDeReuniaoEntity>(sala);
            await _salaReuniaoRepository.AtualizarAsync(salaEntity);
            await _salaReuniaoRepository.SalvarAlteracoesAsync();

            return sala;
        }
    }

}