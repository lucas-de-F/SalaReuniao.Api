using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class SalaServicoOferecidoEntity
    {
        public Guid Id { get; set; }
        public Guid IdServico { get; set; }
        public Guid IdSala { get; set; }
        public decimal Preco { get; set; }
        public string? Observacao { get; set; }

        public ServicoEntity Servico { get; set; } = null!;
        public SalaDeReuniaoEntity Sala { get; set; } = null!;
        public ICollection<ServicoAgendadoEntity> ServicosAgendados { get; set; } = new List<ServicoAgendadoEntity>();
    }
}
