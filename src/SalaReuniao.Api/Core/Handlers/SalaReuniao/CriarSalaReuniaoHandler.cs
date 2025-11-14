using AutoMapper;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.Services;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core
{
    public class CriarSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _repository;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMapper mapper;
        private readonly IEnderecoService _enderecoService;

        public CriarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IUsuarioRepository usuarioRepo, IEnderecoService enderecoService, IMapper mapper)
        {
            _repository = salaDeReuniaoRepository;
            _usuarioRepo = usuarioRepo;
            _enderecoService = enderecoService;
            this.mapper = mapper;
        }
        
        public async Task<SalaDeReuniao> HandleAsync(CriarSalaReuniaoCommand command)
        {
            var usuario = await _usuarioRepo.ObterUsuarioAsync(command.IdResponsavel);
            if (usuario == null)
                throw new DomainException("Responsável não encontrado.");

            var endereco = await _enderecoService.ConsultarCepAsync(command.Endereco.CEP);

            var enderecoCompleto = new Endereco(
                new DadosEndereco
                {
                    Bairro = endereco.Bairro,
                    Localidade = endereco.Localidade,
                    Rua = endereco.Rua,
                    CEP = endereco.CEP,
                    Estado = endereco.Estado
                },
                new DadosComplementaresEndereco
                {
                    Numero = command.Endereco.Numero,
                    Complemento = command.Endereco.Complemento
                }
            );

            var sala = new SalaDeReuniao
            (
                Guid.NewGuid(),
                command.IdResponsavel,
                command.Nome.Trim(),
                command.Capacidade,
                command.ValorHora,
                enderecoCompleto,
                command.Descricao
            );

            var salaEntity = mapper.Map<SalaDeReuniaoEntity>(sala);
            await _repository.AdicionarAsync(salaEntity);
            await _repository.SalvarAlteracoesAsync();

            return sala;
        }
    }

}