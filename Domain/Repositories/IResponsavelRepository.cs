using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Domain.Repositories
{
    public interface IResponsavelRepository
    {
        Task<ResponsavelEntity?> ObterResponsavelAsync(Guid id);
        Task AdicionarAsync(ResponsavelEntity responsavel);
        Task<ICollection<ResponsavelEntity>> ObterResponsaveisAsync();
        Task SalvarAlteracoesAsync();
    }
}
