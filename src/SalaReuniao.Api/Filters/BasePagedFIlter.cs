namespace SalaReuniao.Api.Core.Queries;

public abstract class BasePagedFilter
{
    public int Page { get; set; } = 1;
    public int PageSize { get; set; } = 10;
}
