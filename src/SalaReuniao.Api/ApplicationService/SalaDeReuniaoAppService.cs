using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Services;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObjects;
using SalaReuniao.Domain.Exceptions;
using AutoMapper;

public class SalaDeReuniaoAppService
{
    private readonly ISalaDeReuniaoRepository _salaRepo;
    private readonly IEnderecoService _enderecoService;
    private readonly SalaDeReuniaoService _domainService;
    private readonly IMapper _mapper;
    private readonly IUsuarioRepository _usuarioRepo;
    public SalaDeReuniaoAppService(
        ISalaDeReuniaoRepository salaRepo,
        IEnderecoService enderecoService,
        SalaDeReuniaoService domainService,
        IMapper mapper,
        IUsuarioRepository usuarioRepo)
    {
        _salaRepo = salaRepo;
        _enderecoService = enderecoService;
        _domainService = domainService;
        _mapper = mapper;
        _usuarioRepo = usuarioRepo;
    }

    public async Task<SalaDeReuniao> AtualizarDadosDaSalaAsync(
        AtualizarSalaReuniaoCommand command,
        Guid idProprietario
    )
    {
        var salaEntity = await _salaRepo.ObterPorIdAsync(command.Id)
            ?? throw new DomainException("Sala de reunião não encontrada.");

        var sala = _mapper.Map<SalaDeReuniao>(salaEntity);

        DadosEndereco? dadosCep = null;

        if (command.Endereco.CEP != sala.Endereco.CEP)
            dadosCep = await _enderecoService.ConsultarCepAsync(command.Endereco.CEP);

        _domainService.AtualizarSala(sala, command, dadosCep, idProprietario);

        var updatedEntity = _mapper.Map<SalaDeReuniaoEntity>(sala);

        await _salaRepo.AtualizarAsync(updatedEntity);
        await _salaRepo.SalvarAlteracoesAsync();

        return sala;
    }


    public async Task<SalaDeReuniao> CriarSalaDeReuniaoAsync(
        CriarSalaReuniaoCommand command
    )
    {
        var usuario = await _usuarioRepo.ObterUsuarioAsync(command.IdResponsavel);
            if (usuario == null)
                throw new DomainException("Responsável não encontrado.");

        var endereco = await _enderecoService.ConsultarCepAsync(command.Endereco.CEP);

        var sala = _domainService.CriaSala(command, endereco);

        var salaEntity = _mapper.Map<SalaDeReuniaoEntity>(sala);

        await _salaRepo.AdicionarAsync(salaEntity);
        await _salaRepo.SalvarAlteracoesAsync();

        return sala;
    }
}
