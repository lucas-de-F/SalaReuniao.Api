using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Domain.Repositories
{
    public interface ISalaDeReuniaoRepository
    {
        Task AdicionarAsync(SalaDeReuniaoEntity sala);
        Task<ICollection<SalaDeReuniaoEntity>> ObterPorIdResponsavelAsync(Guid idResponsavel);
        Task SalvarAlteracoesAsync();
        Task RemoverAsync(SalaDeReuniaoEntity sala);
    }
}
