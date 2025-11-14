using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Api.Infrastructure.Extensions;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class SalaDeReuniaoRepository : ISalaDeReuniaoRepository 
    {
        private readonly AppDbContext _context;

        public SalaDeReuniaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task AdicionarAsync(SalaDeReuniaoEntity sala)
        {
            await _context.Salas.AddAsync(sala);
        }

        public async Task AtualizarAsync(SalaDeReuniaoEntity sala)
        {
            _context.Salas.Update(sala);
            await Task.CompletedTask;
        }

        public async Task<SalaDeReuniaoEntity?> ObterPorIdAsync(Guid id)
        {
            return await _context.Salas
                .Include(s => s.ServicosOferecidos)
                .Include(s => s.ReunioesAgendadas)
                .AsNoTracking()
                .FirstOrDefaultAsync(s => s.Id == id);
        }
        
        public async Task<PagedResult<SalaDeReuniaoEntity>> ObterTodasAsync(
            Guid? id = null,
            Guid? idProprietario = null,
            string? nome = null,
            int page = 1,
            int pageSize = 10
        )
        {
            var query = _context.Salas
                .Include(s => s.ServicosOferecidos)
                .Include(s => s.ReunioesAgendadas)
                .AsNoTracking()
                .AsQueryable();
            if (id.HasValue)
                query = query.Where(s => s.Id == id.Value);

            if (idProprietario.HasValue)
                query = query.Where(s => s.IdResponsavel == idProprietario.Value);

            if (!string.IsNullOrWhiteSpace(nome))
                query = query.Where(s => s.Nome.Contains(nome));


            return await query.PaginateAsync(page, pageSize);
        }

        public async Task RemoverAsync(SalaDeReuniaoEntity sala)
        {
            _context.Salas.Remove(sala);
            await Task.CompletedTask;
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}
