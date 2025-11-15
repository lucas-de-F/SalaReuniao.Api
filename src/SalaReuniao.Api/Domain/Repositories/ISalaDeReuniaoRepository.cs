using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Domain.Repositories
{
    public interface ISalaDeReuniaoRepository
    {
        Task AdicionarAsync(SalaDeReuniaoEntity sala);
        Task AtualizarAsync(SalaDeReuniaoEntity sala);
        Task<SalaDeReuniaoEntity?> ObterPorIdAsync(Guid id);
        Task<PagedResult<SalaDeReuniaoEntity>> ObterTodasAsync(FilterSalaReuniao filter);
        Task SalvarAlteracoesAsync();
        Task RemoverAsync(SalaDeReuniaoEntity sala);
    }
}

