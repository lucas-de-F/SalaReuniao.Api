using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Api.Core.Queries;

public class ListarAgendamentosFilter : BasePagedFilter
{
        public Guid? IdCliente { get; set; }
        public Guid? IdSalaReuniao { get; set; }
        public ReuniaoStatus? Status { get; set; }
}
