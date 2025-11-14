using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Domain.Repositories
{
    public interface ISalaDeReuniaoRepository
    {
        Task AdicionarAsync(SalaDeReuniaoEntity sala);
        Task AtualizarAsync(SalaDeReuniaoEntity sala);
        Task<SalaDeReuniaoEntity?> ObterPorIdAsync(Guid id);
        Task<PagedResult<SalaDeReuniaoEntity>> ObterTodasAsync(
            Guid? id = null,
            Guid? idProprietario = null,
            string? nome = null,
            int page = 1,
            int pageSize = 10
        );
        Task SalvarAlteracoesAsync();
        Task RemoverAsync(SalaDeReuniaoEntity sala);
    }
}
