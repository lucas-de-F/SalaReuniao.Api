using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Core
{
    public class ReuniaoAgendada
    {
        public Guid Id { get; set; }
        public Guid IdSalaReuniao { get; set; }
        public Guid IdCliente { get; set; }
        public DateTime Inicio { get; set; }
        public DateTime Fim { get; set; }
        public DateTime Data { get; set; }

        public decimal Duracao => (decimal)(Fim - Inicio).TotalHours;

        public SalaDeReuniao SalaReuniao { get; set; } = null!;
        public ICollection<ServicoAgendado> ServicosAgendados { get; set; } = new List<ServicoAgendado>();
    }
}
