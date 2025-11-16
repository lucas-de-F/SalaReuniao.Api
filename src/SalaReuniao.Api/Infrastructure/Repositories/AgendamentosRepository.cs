using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Api.Infrastructure.Extensions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class AgendamentosRepository : IAgendamentosRepository
    {
        private readonly AppDbContext _context;
        public AgendamentosRepository(AppDbContext context)
        {
            _context = context;
        }

        public Task<PagedResult<ReuniaoAgendadaEntity>> ObterAgendamentosAsync(FilterAgendamentos filter)
        {
            var query = _context.Reunioes.AsNoTracking()
                .Where(s => !filter.IdCliente.HasValue || s.IdCliente == filter.IdCliente.Value)
                .Where(s => !filter.IdSalaReuniao.HasValue || s.IdSalaReuniao == filter.IdSalaReuniao.Value)
                .Where(s => !filter.Status.HasValue || s.Status == filter.Status.Value);

            return query.PaginateAsync(filter.Page, filter.PageSize);
        }

        public async Task<ICollection<DisponibilidadeEntity>> ObterDisponibilidadesAsync()
        {
            return await _context.Disponibilidades.ToListAsync();
        }

    }
}