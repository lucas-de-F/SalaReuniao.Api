using System;

namespace SalaReuniao.Api.Core
{
    public class ServicoAgendado
    {
        public Guid Id { get; set; }
        public Guid IdSalaServicoOferecido { get; set; }
        public Guid IdReuniaoAgendada { get; set; }
        public int Quantidade { get; set; }

        public SalaServicoOferecido SalaServicoOferecido { get; set; } = null!;
        public ReuniaoAgendada ReuniaoAgendada { get; set; } = null!;

        public decimal ValorTotal() => Quantidade * SalaServicoOferecido.Preco;
    }
}
