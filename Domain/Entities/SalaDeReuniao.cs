using System;
using System.Collections.Generic;
using Microsoft.IdentityModel.Tokens;
using SalaReuniao.Api.Core.Commands;
using SalaReuniao.Domain.Exceptions;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core
{
    public class SalaDeReuniao
    {
        public Guid Id { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Endereco  Endereco { get; set; } = new Endereco("", "", "", "", "", "");
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
            Validar(command.Nome, command.Capacidade, command.ValorHora);

            return new SalaDeReuniao
            {
                Id = Guid.NewGuid(),
                Nome = command.Nome.Trim(),
                Capacidade = command.Capacidade,
                Endereco = command.Endereco,
                IdResponsavel = command.IdResponsavel,
                Descricao = command.Descricao,
                ValorHora = command.ValorHora,
            };
        }

        public SalaDeReuniao Atualizar(AtualizarSalaReuniaoCommand command)
        {
            Validar(command.Nome, command.Capacidade, command.ValorHora);
            return new SalaDeReuniao
            {
                Id = command.Id,
                Nome = command.Nome.Trim(),
                Capacidade = command.Capacidade,
                Endereco = command.Endereco,
                IdResponsavel = command.IdResponsavel,
                Descricao = command.Descricao,
                ValorHora = command.ValorHora,
            };
        }

        private static void Validar(string nome, int capacidade, decimal valorHora)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome da sala é obrigatório.");

            if (capacidade <= 0)
                throw new DomainException("A sala deve ter uma capacidade positiva.");

            if (valorHora < 0)
                throw new DomainException("O valor por hora não pode ser negativo.");
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
