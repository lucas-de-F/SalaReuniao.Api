using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Domain.Repositories
{
    public interface IUsuarioRepository
    {
        Task<UsuarioEntity?> ObterUsuarioAsync(Guid id);
        Task<UsuarioEntity?> ObterUsuarioAsync(string nomeUsuario);
        Task AdicionarAsync(UsuarioEntity usuario);
        Task<ICollection<UsuarioEntity>> ObterUsuariosAsync();
        Task SalvarAlteracoesAsync();
    }
}
