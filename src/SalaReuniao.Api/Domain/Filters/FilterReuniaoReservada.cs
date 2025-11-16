using SalaReuniao.Api.Core.Queries;

namespace SalaReuniao.Api.Domain.Filters
{
public class FilterReuniaoReservada : BasePagedFilter
    {
        public Guid? Id { get; set; }
        public Guid? IdSala { get; set; }
    }
}