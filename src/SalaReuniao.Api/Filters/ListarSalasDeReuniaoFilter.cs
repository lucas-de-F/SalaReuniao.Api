namespace SalaReuniao.Api.Core.Queries;

public class ListarSalasDeReuniaoFilter : BasePagedFilter
{
    public Guid? Id { get; set; }
    public Guid? IdProprietario { get; set; }
    public string? Nome { get; set; }
}
