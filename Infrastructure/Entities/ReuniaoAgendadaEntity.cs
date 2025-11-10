using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ReuniaoAgendadaEntity
    {
        public Guid Id { get; set; }
        public Guid IdSalaReuniao { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime Data { get; set; }

        public SalaDeReuniaoEntity SalaReuniao { get; set; } = null!;
        public ICollection<ServicoAgendadoEntity> ServicosAgendados { get; set; } = new List<ServicoAgendadoEntity>();
    }
}
