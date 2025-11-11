using System;

namespace SalaReuniao.Api.Core
{
    public class SalaServicoOferecido
    {
        public Guid Id { get; set; }
        public Guid IdServico { get; set; }
        public Guid IdSala { get; set; }
        public decimal Preco { get; set; }
        public string? Observacao { get; set; }

        public Servico Servico { get; set; } = null!;
        public SalaDeReuniao Sala { get; set; } = null!;
        public ICollection<ServicoAgendado> ServicosAgendados { get; set; } = new List<ServicoAgendado>();
    }
}
