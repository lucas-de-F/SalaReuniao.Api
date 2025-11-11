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
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMapper mapper;

        public AtualizarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IUsuarioRepository usuarioRepo, IMapper mapper)
        {
            _salaReuniaoRepository = salaDeReuniaoRepository;
            _usuarioRepo = usuarioRepo;
            this.mapper = mapper;
        }
        
        public async Task<SalaDeReuniao> HandleAsync(AtualizarSalaReuniaoCommand command, Guid IdProprietario)
        {
            if (command.IdResponsavel != IdProprietario)
                throw new DomainException("Você não tem permissão para atualizar esta sala de reunião.");
                
            var usuario = await _usuarioRepo.ObterUsuarioAsync(command.IdResponsavel);
            if (usuario == null)
                throw new DomainException("Responsável não encontrado.");

            var salaReuniaoEntity = await _salaReuniaoRepository.ObterPorIdAsync(command.Id);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");

            var salaReuniao = mapper.Map<SalaDeReuniao>(salaReuniaoEntity);
            salaReuniao.Atualizar(command);

            var salaEntity = mapper.Map<SalaDeReuniaoEntity>(salaReuniao);
            await _salaReuniaoRepository.AtualizarAsync(salaEntity);
            await _salaReuniaoRepository.SalvarAlteracoesAsync();

            return salaReuniao;
        }
    }

}