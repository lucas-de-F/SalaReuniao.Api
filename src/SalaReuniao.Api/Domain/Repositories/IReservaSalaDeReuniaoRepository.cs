using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Domain.Repositories
{
    public interface IReservaSalaDeReuniaoRepository
    {
        Task ReservarSalaAsync(ReuniaoAgendadaEntity sala);
        Task<PagedResult<ReuniaoAgendadaEntity>> ObterTodasAsync(FilterReuniaoReservada filter);
        Task SalvarAlteracoesAsync();
    }
}

