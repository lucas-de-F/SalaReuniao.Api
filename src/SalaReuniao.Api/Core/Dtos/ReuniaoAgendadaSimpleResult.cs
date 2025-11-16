using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Api.Core.Dtos
{
    public class ReuniaoAgendadaSimpleResult
    {
        public Guid Id { get; set; }
        public Guid IdSalaReuniao { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
        public DateOnly Data { get; set; }
        public ReuniaoStatus Status { get; set; }
    }
}
