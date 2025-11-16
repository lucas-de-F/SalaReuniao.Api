using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class UsuarioRepository : IUsuarioRepository
    {
        private readonly AppDbContext _context;
        public UsuarioRepository(AppDbContext context)
        {
            _context = context;
        }
        public async Task AdicionarAsync(UsuarioEntity usuario)
        {
            await _context.Usuarios.AddAsync(usuario);
        }

        public async Task<ICollection<UsuarioEntity>> ObterUsuariosAsync()
        {
            return await _context.Usuarios.ToListAsync();
        }

        public async Task<UsuarioEntity?> ObterUsuarioAsync(Guid id)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(r => r.Id == id);
        }
        public async Task<UsuarioEntity?> ObterUsuarioAsync(string nome)
        {
            return await _context.Usuarios.FirstOrDefaultAsync(r => r.Nome == nome);
        }
        public async Task SalvarAlteracoesAsync()
        {
            await _context.SaveChangesAsync();
        }
    }
}