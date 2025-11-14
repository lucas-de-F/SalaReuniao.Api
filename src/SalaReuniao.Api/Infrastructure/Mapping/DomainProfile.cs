using AutoMapper;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Infrastructure.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // Usuário e subclasses
            CreateMap<Usuario, UsuarioEntity>().ReverseMap();
            CreateMap<Cliente, UsuarioEntity>().ReverseMap();
            CreateMap<Responsavel, UsuarioEntity>().ReverseMap();
            CreateMap<ResponsavelResult, Responsavel>().ReverseMap();

            // Salas
            CreateMap<SalaDeReuniao, SalaDeReuniaoEntity>().ReverseMap();
            CreateMap<SalaDeReuniaoResult, SalaDeReuniao>().ReverseMap();
            CreateMap<SalaDeReuniaoEntity, SalaDeReuniaoResult>().ReverseMap();
            CreateMap<Endereco, EnderecoResult>().ReverseMap();

            // Serviços
            CreateMap<Servico, ServicoEntity>().ReverseMap();
            CreateMap<SalaServicoOferecido, SalaServicoOferecidoEntity>().ReverseMap();
            CreateMap<ServicoAgendado, ServicoAgendadoEntity>().ReverseMap();

            // Reuniões
            CreateMap<ReuniaoAgendada, ReuniaoAgendadaEntity>().ReverseMap();
        }
    }
}
