using System;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ServicoAgendadoEntity
    {
        public Guid Id { get; set; }
        public Guid IdSalaServicoOferecido { get; set; }
        public Guid IdReuniaoAgendada { get; set; }
        public int Quantidade { get; set; }

        public SalaServicoOferecidoEntity SalaServicoOferecido { get; set; } = null!;
        public ReuniaoAgendadaEntity ReuniaoAgendada { get; set; } = null!;
    }
}
