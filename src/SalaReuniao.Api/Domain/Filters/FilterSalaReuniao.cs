using SalaReuniao.Api.Core.Queries;

namespace SalaReuniao.Api.Domain.Filters
{
public class FilterSalaReuniao : BasePagedFilter
    {
        public Guid? Id { get; set; }
        public Guid? IdProprietario { get; set; }
        public string? Nome { get; set; }
        public DateOnly? Data { get; set; }
        public TimeOnly? HoraInicio { get; set; }
        public double? Duracao { get; set; }
        public int? Capacidade { get; set; }
        public string[] Estado { get; set; } = Array.Empty<string>();
        public string[] Municipio { get; set; } = Array.Empty<string>();
    }
}