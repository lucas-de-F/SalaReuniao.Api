using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Api.Infrastructure.Extensions;
using SalaReuniao.Domain.ValueObject;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Domain.Filters;

namespace SalaReuniao.Api.Infrastructure.Repositories
{
    public class LocalidadesSalaReuniaoRepository : ILocalidadesSalaReuniaoRepository
    {
        private readonly AppDbContext _context;

        public LocalidadesSalaReuniaoRepository(AppDbContext context)
        {
            _context = context;
        }

        public async Task<PagedResult<LocalidadeResult>> ObterFiltrosLocalidade(FilterLocalidade filter)
        {
            var query = _context.Salas
                .AsNoTracking()
                .Select(s => new 
                {
                    Estado = s.Endereco.Estado,
                    Municipio = s.Endereco.Municipio
                })
                .GroupBy(x => x.Estado)
                .Where(g => string.IsNullOrWhiteSpace(filter.Estado) || g.Key.Contains(filter.Estado))
                .Select(g => new LocalidadeResult
                {
                    Estado = g.Key,
                    Municipios = g.Select(x => x.Municipio)
                                .Distinct()
                                .OrderBy(m => m)
                                .ToList()
                });


            return await query.PaginateAsync(filter.Page, filter.PageSize);

        }
    }
}
