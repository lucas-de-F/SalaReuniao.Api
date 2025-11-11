using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;

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

        public Task<SalaDeReuniaoEntity?> ObterPorIdAsync(Guid id)
        {
            return _context.Salas
                .Include(s => s.ServicosOferecidos)
                .Include(s => s.ReunioesAgendadas)
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        public async Task<ICollection<SalaDeReuniaoEntity>> ObterPorIdResponsavelAsync(Guid idResponsavel)
        {
            return await _context.Salas
                .Include(s => s.ServicosOferecidos)
                .Include(s => s.ReunioesAgendadas)
                .Where(s => s.IdResponsavel == idResponsavel)
                .ToListAsync();
        }

        public async Task<ICollection<SalaDeReuniaoEntity>> ObterTodasAsync(Guid? idProprietario = null, string? nome = null)
        {
            var query = _context.Salas
                .Include(s => s.ServicosOferecidos)
                .Include(s => s.ReunioesAgendadas)
                .AsQueryable();

            if (idProprietario.HasValue)
            {
                query = query.Where(s => s.IdResponsavel == idProprietario.Value);
            }

            if (!string.IsNullOrEmpty(nome))
            {
                query = query.Where(s => s.Nome.Contains(nome));
            }

            return await query.ToListAsync();
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
