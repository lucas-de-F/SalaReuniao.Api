using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Domain.Repositories
{
    public interface IAgendamentosRepository
    {
        Task<PagedResult<ReuniaoAgendadaEntity>> ObterAgendamentosAsync(FilterAgendamentos filter);
    }
}

