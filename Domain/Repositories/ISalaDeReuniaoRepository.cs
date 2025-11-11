using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Domain.Repositories
{
    public interface ISalaDeReuniaoRepository
    {
        Task AdicionarAsync(SalaDeReuniaoEntity sala);
        Task AtualizarAsync(SalaDeReuniaoEntity sala);
        Task<ICollection<SalaDeReuniaoEntity>> ObterPorIdResponsavelAsync(Guid idResponsavel);
        Task<SalaDeReuniaoEntity?> ObterPorIdAsync(Guid id);

        Task<ICollection<SalaDeReuniaoEntity>> ObterTodasAsync(Guid? idProprietario = null, string? nome = null);
        Task SalvarAlteracoesAsync();
        Task RemoverAsync(SalaDeReuniaoEntity sala);
    }
}
