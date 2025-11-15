using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class DisponibilidadeRepository : IDisponibilidadeRepository
    {
        private readonly AppDbContext _context;
        public DisponibilidadeRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(DisponibilidadeEntity Disponibilidade)
        {
            await _context.Disponibilidades.AddAsync(Disponibilidade);
        }
        public async Task<ICollection<DisponibilidadeEntity>> ObterDisponibilidadesAsync()
        {
            return await _context.Disponibilidades.ToListAsync();
        }
        public async Task<DisponibilidadeEntity?> ObterDisponibilidadeByIdAsync(Guid id)
        {
            return await _context.Disponibilidades.Where(d => d.Id == id).FirstOrDefaultAsync();
        }
        public async Task<DisponibilidadeEntity?> ObterDisponibilidadeBySalaReuniaoIdAsync(Guid id)
        {
            return await _context.Disponibilidades.Where(d => d.SalaDeReuniaoId == id).FirstOrDefaultAsync();
        }
        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }


    }
}