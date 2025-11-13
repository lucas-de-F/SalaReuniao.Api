using System;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class DisponibilidadeEntity
    {
        public Guid Id { get; set; }
        public Guid SalaDeReuniaoId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeSpan Inicio { get; set; }
        public TimeSpan Fim { get; set; }

        public SalaDeReuniaoEntity SalaDeReuniao { get; set; } = null!;
    }
}
