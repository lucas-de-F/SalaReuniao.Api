using SalaReuniao.Api.Core.Dtos;
using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.ValueObject;

namespace SalaReuniao.Domain.Repositories
{
    public interface ISalaDeReuniaoRepository
    {
        Task AdicionarAsync(SalaDeReuniaoEntity sala);
        Task AtualizarAsync(SalaDeReuniaoEntity sala);
        Task<SalaDeReuniaoEntity?> ObterPorIdAsync(Guid id);
        Task<PagedResult<SalaDeReuniaoEntity>> ObterTodasAsync(FilterSalaReuniao filter);
        Task<PagedResult<FiltrosLocalidadeResult>> ObterFiltrosLocalidade(FilterLocalidade filter);
        Task SalvarAlteracoesAsync();
        Task RemoverAsync(SalaDeReuniaoEntity sala);
    }
    public class FilterSalaReuniao : BasePagedFilter
    {
        public Guid? Id { get; set; }
        public Guid? IdProprietario { get; set; }
        public string? Nome { get; set; }
        public DateOnly? Data { get; set; }
        public TimeOnly? HoraInicio { get; set; }
        public TimeOnly? Duracao { get; set; }
        public int? Capacidade { get; set; }
        public string Estado { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
    }

    public class FilterLocalidade : BasePagedFilter
    {
        public string Estado { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
    }
}

