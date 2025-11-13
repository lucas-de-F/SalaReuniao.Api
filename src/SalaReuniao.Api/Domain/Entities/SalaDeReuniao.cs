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
        public Guid IdResponsavel { get; private set; }
        public string Nome { get; private  set; } = string.Empty;
        public int Capacidade { get; private  set; }
        public string Descricao { get; private set; } = string.Empty;
        public Endereco Endereco { get; private set; }
        public decimal ValorHora { get; private  set; }
        public DisponibilidadeSemanal DisponibilidadeSemanal { get; private set; } = DisponibilidadeSemanal.Padrao();

        public Responsavel Responsavel { get; set; } = null!;
        public ICollection<ReuniaoAgendada> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendada>();
        public ICollection<SalaServicoOferecido> ServicosOferecidos { get; set; } = new List<SalaServicoOferecido>();

        public SalaDeReuniao(Guid id, Guid idResponsavel, string nome, int capacidade, decimal valorHora, Endereco endereco, string descricao = "")
        {
            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            IdResponsavel = idResponsavel;
            Descricao = descricao;
            Nome = nome;
            Capacidade = capacidade;
            ValorHora = valorHora;
            Endereco = endereco;
        }
        public bool AgendaDisponivel(DateTime inicio, DateTime fim)
        {
            if (inicio >= fim)
                throw new DomainException("O horário inicial deve ser anterior ao final.");

            bool conflito = ReunioesAgendadas.Any(r => inicio < r.Fim && fim > r.Inicio);
            if (conflito)
                return false;

            return DisponibilidadeSemanal.EstaDisponivel(inicio, fim);
        }

        public static SalaDeReuniao Criar(CriarSalaReuniaoCommand command)
        {
            Validar(command.Nome, command.Capacidade, command.ValorHora);

            return new SalaDeReuniao
            (
                Guid.NewGuid(),
                command.IdResponsavel,
                command.Nome.Trim(),
                command.Capacidade,
                command.ValorHora,
                command.Endereco,
                command.Descricao
            );
        }

        public void Atualizar(AtualizarSalaReuniaoCommand command)
        {
            Validar(command.Nome, command.Capacidade, command.ValorHora);

            Nome = command.Nome?.Trim() ?? Nome;
            Capacidade = command.Capacidade != 0 ? command.Capacidade : Capacidade;
            ValorHora = command.ValorHora != 0 ? command.ValorHora : ValorHora;
            Descricao = command.Descricao ?? Descricao;
            DisponibilidadeSemanal = command.DisponibilidadeSemanal ?? DisponibilidadeSemanal;
            
            if (command.Endereco != null)
                Endereco.Atualizar(command.Endereco);
        }

        private static void Validar(string nome, int capacidade, decimal valorHora)
        {
            if (string.IsNullOrWhiteSpace(nome))
                throw new DomainException("O nome da sala é obrigatório.");

            if (capacidade < 1)
                throw new DomainException("A sala deve comportar ao menos uma pessoa.");

            if (valorHora < 0)
                throw new DomainException("O valor por hora não pode ser negativo.");
        }
        public void AgendaReuniao(Guid clienteId, DateTime inicio, DateTime fim)
        {
            if (!AgendaDisponivel(inicio, fim))
                throw new DomainException("A sala não está disponível no horário e dia solicitado.");

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
