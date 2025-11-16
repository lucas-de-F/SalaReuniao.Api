using SalaReuniao.Domain.ValueObjects;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;

namespace SalaReuniao.Domain.Services;

public class SalaDeReuniaoService
{
    public SalaDeReuniao CriaSala(
        CriarSalaReuniaoCommand salaCriacao,
        DadosEndereco? dadosEndereco
    )
    {
        // Regras de endereço
        if (salaCriacao.Endereco.CEP == null)
            throw new DomainException("CEP é obrigatório.");

        // Criação de endereço
        var enderecoCompleto = new Endereco(
                new DadosEndereco
                {
                    Bairro = dadosEndereco.Bairro,
                    Municipio = dadosEndereco.Municipio,
                    Rua = dadosEndereco.Rua,
                    CEP = dadosEndereco.CEP,
                    Estado = dadosEndereco.Estado
                },
                new DadosComplementaresEndereco
                {
                    Numero = salaCriacao.Endereco.Numero,
                    Complemento = salaCriacao.Endereco.Complemento
                }
            );
        // Definição dos dados básicos
        
        return new SalaDeReuniao
            (
                Guid.NewGuid(),
                salaCriacao.IdResponsavel,
                salaCriacao.Nome.Trim(),
                salaCriacao.Capacidade,
                salaCriacao.ValorHora,
                enderecoCompleto,
                salaCriacao.Descricao,
                salaCriacao.DisponibilidadeSemanal
            );



    }
    public void AtualizarSala(
        SalaDeReuniao sala,
        AtualizarSalaReuniaoCommand salaAtualizacao,
        DadosEndereco? dadosEndereco,
        Guid idProprietario
    )
    {
        // Regras de permissão
        if (idProprietario != sala.IdResponsavel)
            throw new DomainException("Você não tem permissão para atualizar esta sala.");

        // Regras de endereço
        if (salaAtualizacao.Endereco.CEP == null)
            throw new DomainException("CEP é obrigatório.");

        // Atualização de endereço
        sala.AtualizarEndereco(
            dadosEndereco,
            new DadosComplementaresEndereco
            {
                Numero = salaAtualizacao.Endereco.Numero,
                Complemento = salaAtualizacao.Endereco.Complemento
            });

        // Atualização dos dados básicos
        sala.Atualizar(
            salaAtualizacao.Nome,
            salaAtualizacao.Capacidade,
            salaAtualizacao.ValorHora,
            salaAtualizacao.Descricao,
            salaAtualizacao.DisponibilidadeSemanal
        );
    }
}
