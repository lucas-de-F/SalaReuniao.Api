using System;
using System.Collections.Generic;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Api.Core
{
    public class SalaDeReuniao
    {
        public Guid Id { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
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

        public static SalaDeReuniao Criar(CriarSalaReuniaoCommand command)
        {
            if (string.IsNullOrWhiteSpace(command.Nome))
                throw new DomainException("O nome da sala é obrigatório.");

            if (command.Capacidade <= 0)
                throw new DomainException("A sala deve ter uma capacidade positiva.");
            
            return new SalaDeReuniao
            {
                Id = Guid.NewGuid(),
                Nome = command.Nome.Trim(),
                Capacidade = command.Capacidade,
                IdEndereco = command.IdEndereco,
                IdResponsavel = command.IdResponsavel,
                Descricao = command.Descricao,
                ValorHora = command.ValorHora,
            };
        }

        public void AgendaReuniao(Guid clienteId, DateTime inicio, DateTime fim)
        {
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
