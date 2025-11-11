using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class ResponsavelRepository : IResponsavelRepository
    {
        private readonly AppDbContext _context;
        public ResponsavelRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(ResponsavelEntity responsavel)
        {
            await _context.Responsaveis.AddAsync(responsavel);
        }

        public async Task<ICollection<ResponsavelEntity>> ObterResponsaveisAsync()
        {
            return await _context.Responsaveis.ToListAsync();
        }

        public async Task<ResponsavelEntity?> ObterResponsavelAsync(Guid id)
        {
            return await _context.Responsaveis.FirstOrDefaultAsync(r => r.Id == id);
        }

        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}