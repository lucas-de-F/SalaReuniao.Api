using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Domain.Filters;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Api.Infrastructure.Extensions;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class ReservaSalaDeReuniaoRepository : IReservaSalaDeReuniaoRepository
    {
        private readonly AppDbContext _context;
        public ReservaSalaDeReuniaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<ReuniaoAgendadaEntity>> ObterTodasAsync(FilterReuniaoReservada filter)
        {
            var query = _context.Reunioes.AsNoTracking()
                .Include(r => r.Cliente)
                .Include(r => r.SalaReuniao)
                .Where(r => !filter.Id.HasValue || r.Id == filter.Id.Value)
                .Where(r => !filter.IdSala.HasValue || r.IdSalaReuniao == filter.IdSala.Value)
                .Where(r => !filter.IdCliente.HasValue || r.IdCliente == filter.IdCliente.Value)
                .OrderBy(r => r.Data);

            return await query.PaginateAsync(filter.Page, filter.PageSize);
        }

        public async Task ReservarSalaAsync(ReuniaoAgendadaEntity reuniao)
        {
            await _context.Reunioes.AddAsync(reuniao);
        }
        public Task AtualizarReservarSalaAsync(ReuniaoAgendadaEntity reuniao)
        {
            var entry = _context.Reunioes.Local.FirstOrDefault(x => x.Id == reuniao.Id);

            if (entry != null)
            {
                // Atualiza os valores na entidade já rastreada
                _context.Entry(entry).CurrentValues.SetValues(reuniao);
            }
            else
            {
                // Anexa a entidade como modificada
                _context.Reunioes.Attach(reuniao);
                _context.Entry(reuniao).State = EntityState.Modified;
            }

            return Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}