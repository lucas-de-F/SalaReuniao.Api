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
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMapper mapper;

        public CriarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IUsuarioRepository usuarioRepo, IMapper mapper)
        {
            _repository = salaDeReuniaoRepository;
            _usuarioRepo = usuarioRepo;
            this.mapper = mapper;
        }
        
        public async Task<SalaDeReuniao> HandleAsync(CriarSalaReuniaoCommand command)
        {
            var usuario = await _usuarioRepo.ObterUsuarioAsync(command.IdResponsavel);
            if (usuario == null)
                throw new DomainException("Responsável não encontrado.");

            var sala = SalaDeReuniao.Criar(command);

            var salaEntity = mapper.Map<SalaDeReuniaoEntity>(sala);
            await _repository.AdicionarAsync(salaEntity);
            await _repository.SalvarAlteracoesAsync();

            return sala;
        }
    }

}