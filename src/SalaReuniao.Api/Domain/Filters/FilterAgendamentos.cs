using SalaReuniao.Api.Core.Queries;
using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Api.Domain.Filters
{
public class FilterAgendamentos : BasePagedFilter
    {
        public Guid? IdCliente { get; set; }
        public Guid? IdSalaReuniao { get; set; }
        public ReuniaoStatus? Status { get; set; }
    }
}