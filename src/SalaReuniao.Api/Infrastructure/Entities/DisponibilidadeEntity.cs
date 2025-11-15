using System;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class DisponibilidadeEntity
    {
        public Guid Id { get; set; }
        public Guid SalaDeReuniaoId { get; set; }
        public DayOfWeek DiaSemana { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
        public SalaDeReuniaoEntity SalaDeReuniao { get; set; } = null!;
    }
}
