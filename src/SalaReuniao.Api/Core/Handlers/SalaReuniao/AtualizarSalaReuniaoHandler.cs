using AutoMapper;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.Services;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core
{
    public class AtualizarSalaReuniaoHandler
    {
        private readonly ISalaDeReuniaoRepository _repository;
        private readonly IUsuarioRepository _usuarioRepo;
        private readonly IMapper mapper;
        private readonly IEnderecoService _enderecoService;

        public AtualizarSalaReuniaoHandler(ISalaDeReuniaoRepository salaDeReuniaoRepository, IUsuarioRepository usuarioRepo, IEnderecoService enderecoService, IMapper mapper)
        {
            _repository = salaDeReuniaoRepository;
            _usuarioRepo = usuarioRepo;
            _enderecoService = enderecoService;
            this.mapper = mapper;
        }

        public void ValidaPermissaoParaAtualizar(AtualizarSalaReuniaoCommand command, Guid IdProprietario)
        {
            if (command.IdResponsavel != IdProprietario)
                throw new DomainException("Você não tem permissão para atualizar esta sala de reunião.");
        }
        
        public async Task<SalaDeReuniao> HandleAsync(AtualizarSalaReuniaoCommand command, Guid IdProprietario)
        {
            ValidaPermissaoParaAtualizar(command, IdProprietario);

            var salaReuniaoEntity = await _repository.ObterPorIdAsync(command.Id);
            if (salaReuniaoEntity == null)
                throw new DomainException("Sala de reunião não encontrada.");

            var salaReuniao = mapper.Map<SalaDeReuniao>(salaReuniaoEntity);
            Endereco enderecoCompleto;
                DadosEndereco? dadosEndereco = null;

            if (command.Endereco.CEP != salaReuniao.Endereco.CEP)
            {
                dadosEndereco = await _enderecoService.ConsultarCepAsync(command.Endereco.CEP);
            }
            
            salaReuniao.AtualizarEndereco(
                    dadosEndereco,
                    new DadosComplementaresEndereco
                    {
                            Numero = command.Endereco.Numero,
                            Complemento = command.Endereco.Complemento
                    }
            );

            salaReuniao.Atualizar(
                command.Nome,
                command.Capacidade,
                command.ValorHora,
                command.Descricao,
                command.DisponibilidadeSemanal
            );

            var salaEntity = mapper.Map<SalaDeReuniaoEntity>(salaReuniao);
            await _repository.AtualizarAsync(salaEntity);
            await _repository.SalvarAlteracoesAsync();

            return salaReuniao;
        }
    }

}