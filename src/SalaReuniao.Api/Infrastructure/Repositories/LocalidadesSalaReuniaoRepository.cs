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
            var estados = await _context.Salas
                .AsNoTracking()
                .Where(s => string.IsNullOrWhiteSpace(filter.Estado) || s.Endereco.Estado.Contains(filter.Estado))
                .Select(s => s.Endereco.Estado)
                .Distinct()
                .OrderBy(e => e)
                .ToListAsync();

            var items = new List<LocalidadeResult>();

            foreach (var estado in estados)
            {
                var municipios = await _context.Salas
                    .AsNoTracking()
                    .Where(s => s.Endereco.Estado == estado)
                    .Select(s => s.Endereco.Municipio)
                    .Distinct()
                    .OrderBy(m => m)
                    .ToListAsync();

                items.Add(new LocalidadeResult { Estado = estado, Municipios = municipios });
            }

            return new PagedResult<LocalidadeResult>
            (
                page: 0,
                pageSize: estados.Count,
                totalItems: estados.Count,
                items: items
            );
        }
    }
}
