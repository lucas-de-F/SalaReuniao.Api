namespace SalaReuniao.Api.Core.Queries;

public class ListarLocalidadesFilter : BasePagedFilter
{
    public string Estado { get; set; } = string.Empty;
    public string Municipio { get; set; } = string.Empty;
}
