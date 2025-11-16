using AutoMapper;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Core
{
    public class LoginUsuarioHandler
    {
        private readonly IUsuarioRepository _usuarioRepository;
        private readonly IMapper mapper;

        public LoginUsuarioHandler(IUsuarioRepository usuarioRepository, IMapper mapper)
        {
            _usuarioRepository = usuarioRepository;
            this.mapper = mapper;
        }
        
        public async Task<Guid> HandleAsync(string nomeUsuario)
        {
            var resultado = await _usuarioRepository.ObterUsuarioAsync(
                nomeUsuario
            );
            if (resultado == null)
            {
                throw new KeyNotFoundException("Usuário não encontrado.");
            }

            return resultado.Id;
        }
    }
}