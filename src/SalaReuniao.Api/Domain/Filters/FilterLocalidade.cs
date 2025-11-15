using SalaReuniao.Api.Core.Queries;

namespace SalaReuniao.Api.Domain.Filters
{
    public class FilterLocalidade : BasePagedFilter
    {
        public string Estado { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
    }
}