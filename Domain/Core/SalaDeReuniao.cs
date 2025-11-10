using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Core
{
    public class SalaDeReuniao
    {
        public Guid Id { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid IdEndereco { get; set; }
        public decimal ValorHora { get; set; }

        public Responsavel Responsavel { get; set; } = null!;
        public ICollection<ReuniaoAgendada> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendada>();
        public ICollection<SalaServicoOferecido> ServicosOferecidos { get; set; } = new List<SalaServicoOferecido>();

        public bool VerificaDisponibilidade(DateTime inicio, DateTime fim)
        {
            return !ReunioesAgendadas.Any(r => (inicio < r.Fim && fim > r.Inicio));
        }

        public void AgendaReuniao(Guid clienteId, DateTime inicio, DateTime fim)
        {
            // if (!VerificaDisponibilidade(inicio, fim))
            //     throw new InvalidOperationException("Horário já reservado.");

            var reuniao = new ReuniaoAgendada
            {
                Id = Guid.NewGuid(),
                IdSalaReuniao = Id,
                IdCliente = clienteId,
                Inicio = inicio,
                Fim = fim,
                Data = inicio.Date
            };

            ReunioesAgendadas.Add(reuniao);
        }
    }
}
