
namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ReuniaoAgendadaEntity
    {
        public Guid Id { get; set; }
        public Guid IdSalaReuniao { get; set; }
        public Guid IdCliente { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
        public DateOnly Data { get; set; }
        public ReuniaoStatus Status { get; set; }
        public UsuarioEntity Cliente { get; set; } = null!;
        public SalaDeReuniaoEntity SalaReuniao { get; set; } = null!;
        public ICollection<ServicoAgendadoEntity> ServicosAgendados { get; set; } = new List<ServicoAgendadoEntity>();
    }

    public enum ReuniaoStatus
    {
        Agendada,
        Cancelada,
    }
}
