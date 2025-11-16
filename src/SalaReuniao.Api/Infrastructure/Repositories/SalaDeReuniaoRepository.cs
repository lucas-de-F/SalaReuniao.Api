using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;
using SalaReuniao.Api.Infrastructure.Extensions;
using SalaReuniao.Domain.ValueObject;
using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Domain.Filters;

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
                .Include(s => s.Disponibilidades)
                .Include(s => s.ReunioesAgendadas.Where(r => r.Status != ReuniaoStatus.Cancelada))
                .FirstOrDefaultAsync(s => s.Id == id);
        }

        private void ExcluiSalasComReunioesQueColidemComFiltro(ref IQueryable<SalaDeReuniaoEntity> query, FilterSalaReuniao filter)
        {
            if (filter.Data.HasValue && filter.HoraInicio.HasValue)
            {
                var dataAgenda = filter.Data.Value;
                var dataAgendaComHora = filter.Data.Value.ToDateTime(TimeOnly.MinValue);
                var inicio = filter.HoraInicio.Value;
                var fim = filter.Duracao > 0
                    ? inicio.AddHours(filter.Duracao.Value)
                    : inicio.Add(TimeSpan.FromMinutes(1));

                query = query.Where(s =>
                    !s.ReunioesAgendadas.Any(r =>
                        r.Data == dataAgenda &&
                        (
                            (inicio >= r.Inicio && inicio < r.Fim) ||
                            (fim > r.Inicio && fim <= r.Fim) ||
                            (inicio <= r.Inicio && fim >= r.Fim)
                        )
                    )
                );
            }
        }

        public void FiltraEstadoMunicipio(ref IQueryable<SalaDeReuniaoEntity> query, FilterSalaReuniao filter)
        {
            if (filter.Estado.Length > 0)
                query = query.Include(s => s.Endereco).Where(s => filter.Estado.Contains(s.Endereco.Estado));

            if (filter.Municipio.Length > 0)
                query = query.Include(s => s.Endereco).Where(s => filter.Municipio.Contains(s.Endereco.Municipio));
        }

        public void FiltraPorHorarioDeInicioDeReuniao(ref IQueryable<SalaDeReuniaoEntity> query, FilterSalaReuniao filter)
        {
            if (filter.HoraInicio.HasValue)
            {
                var inicio = filter.HoraInicio.Value;

                query = query.Where(s =>
                    s.Disponibilidades.Any(d =>
                        d.Inicio <= inicio &&
                        d.Fim >= inicio
                    )
                );
            }
        }
        public void FiltraPorInicioEDuracao(ref IQueryable<SalaDeReuniaoEntity> query, FilterSalaReuniao filter)
        {
            if (!filter.HoraInicio.HasValue)
                return;

            var inicio = filter.HoraInicio.Value;
            var inicioSpan = inicio.ToTimeSpan();

            // Aplica apenas o filtro por início
            FiltraPorHorarioDeInicioDeReuniao(ref query, filter);

            // Se não tem duração, acabou
            if (!filter.Duracao.HasValue)
                return;

            var duracaoHoras = filter.Duracao.Value;

            // Duração convertida para TimeSpan
            var duracaoSpan = TimeSpan.FromHours(duracaoHoras);

            // Cálculo manual do fim
            var fimSpan = inicioSpan + duracaoSpan;

            // ❗ SE ESTOUROU 24 HORAS → invalida
            if (fimSpan.TotalHours >= 24)
            {
                // nenhuma sala é válida pois fim passou do dia
                query = query.Where(s => false);
                return;
            }

            // Converte para TimeOnly sem wrap-around
            var fim = TimeOnly.FromTimeSpan(fimSpan);

            // Filtro por disponibilidade
            query = query.Where(s =>
                s.Disponibilidades.Any(d =>
                    d.Inicio <= inicio &&
                    d.Fim >= fim
                )
            );
        }
        public async Task<PagedResult<SalaDeReuniaoEntity>> ObterTodasAsync(FilterSalaReuniao filter)
        {
            var query = _context.Salas
                .Include(s => s.Disponibilidades)
                .Include(s => s.ServicosOferecidos)
                .Include(s => s.ReunioesAgendadas)
                .AsNoTracking()
                .AsQueryable();

            if (filter.Id.HasValue)
                query = query.Where(s => s.Id == filter.Id);

            if (filter.IdProprietario.HasValue)
                query = query.Where(s => s.IdResponsavel == filter.IdProprietario);

            if (!string.IsNullOrWhiteSpace(filter.Nome))
                query = query.Where(s => s.Nome.Contains(filter.Nome));

            if (filter.Capacidade.HasValue)
                query = query.Where(s => s.Capacidade >= filter.Capacidade.Value);
            // FILTRO POR DISPONIBILIDADE SEMANAL
            if (filter.Data.HasValue)
            {
                var diaSemana = filter.Data.Value.DayOfWeek;
                query = query.Where(s => s.Disponibilidades.Any(d => d.DiaSemana == diaSemana));
            }
            if (filter.Capacidade.HasValue)
            {
                FiltraPorHorarioDeInicioDeReuniao(ref query, filter);
            }
            // FILTRO POR INÍCIO
            FiltraPorInicioEDuracao(ref query, filter);
            ExcluiSalasComReunioesQueColidemComFiltro(ref query, filter);
            FiltraEstadoMunicipio(ref query, filter);

            return await query.PaginateAsync(filter.Page, filter.PageSize);
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
