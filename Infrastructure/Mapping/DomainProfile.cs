using AutoMapper;
using SalaReuniao.Api.Core;
using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Api.Infrastructure.Mappings
{
    public class DomainProfile : Profile
    {
        public DomainProfile()
        {
            // Usuário e subclasses
            CreateMap<Usuario, UsuarioEntity>().ReverseMap();
            CreateMap<Cliente, ClienteEntity>().ReverseMap();
            CreateMap<Responsavel, ResponsavelEntity>().ReverseMap();

            // Salas
            CreateMap<SalaDeReuniao, SalaDeReuniaoEntity>().ReverseMap();

            // Serviços
            CreateMap<Servico, ServicoEntity>().ReverseMap();
            CreateMap<SalaServicoOferecido, SalaServicoOferecidoEntity>().ReverseMap();
            CreateMap<ServicoAgendado, ServicoAgendadoEntity>().ReverseMap();

            // Reuniões
            CreateMap<ReuniaoAgendada, ReuniaoAgendadaEntity>().ReverseMap();
        }
    }
}
