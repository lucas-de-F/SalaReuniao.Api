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
        public DisponibilidadeSemanal? DisponibilidadeSemanal { get; set; } = new DisponibilidadeSemanal();

        public Responsavel Responsavel { get; set; } = null!;
        public ICollection<ReuniaoAgendada> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendada>();
        public ICollection<SalaServicoOferecido> ServicosOferecidos { get; set; } = new List<SalaServicoOferecido>();

        public SalaDeReuniao(Guid id, Guid idResponsavel, string nome, int capacidade, decimal valorHora, Endereco endereco, string descricao = "", DisponibilidadeSemanal? disponibilidadeSemanal = null)
        {
            Validar(nome, capacidade, valorHora);

            Id = id == Guid.Empty ? Guid.NewGuid() : id;
            IdResponsavel = idResponsavel;
            Descricao = descricao;
            Nome = nome;
            Capacidade = capacidade;
            ValorHora = valorHora;
            Endereco = endereco;
            DisponibilidadeSemanal = disponibilidadeSemanal ?? new DisponibilidadeSemanal();
        }
        public bool AgendaDisponivel(DateOnly data, TimeOnly inicio, TimeOnly fim)
        {
            if (DisponibilidadeSemanal == null || DisponibilidadeSemanal.Disponibilidades.Count == 0)
                return false;

            if (data < DateOnly.FromDateTime(DateTime.Now))
                return false;
                
            if (inicio.Hour >= fim.Hour)
                throw new DomainException("O horário inicial deve ser anterior ao final.");

            bool conflito = ReunioesAgendadas.Where(r => r.Data == data).Any(r => inicio < r.Fim && fim > r.Inicio);
            if (conflito)
                return false;

            var estaDisponivel = DisponibilidadeSemanal.EstaDisponivel(data, inicio, fim);
            return estaDisponivel;
        }
        public void AtualizarEndereco(DadosEndereco? dadosEndereco, DadosComplementaresEndereco? dadosComplementaresEndereco)
        {
            Endereco.Atualizar(dadosEndereco, dadosComplementaresEndereco);
        } 

        public void Atualizar(string nome, int capacidade, decimal valorHora, string descricao = "", DisponibilidadeSemanal? disponibilidadeSemanal = null)
        {
            Validar(nome, capacidade, valorHora);

            Nome = nome?.Trim() ?? Nome;
            Capacidade = capacidade != 0 ? capacidade : Capacidade;
            ValorHora = valorHora != 0 ? valorHora : ValorHora;
            Descricao = descricao ?? Descricao;
            DisponibilidadeSemanal = disponibilidadeSemanal ?? DisponibilidadeSemanal;
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
        public ReuniaoAgendada AgendaReuniao(Guid clienteId, DateOnly data, TimeOnly inicio, TimeOnly fim)
        {
            if (!AgendaDisponivel(data, inicio, fim))
                throw new DomainException("A sala não está disponível no horário e dia solicitado.");

            var reuniao = new ReuniaoAgendada
            (
                id: Guid.NewGuid(),
                idSalaReuniao: Id,
                idCliente: clienteId,
                inicio: inicio,
                fim: fim,
                data: data
            );

            ReunioesAgendadas.Add(reuniao);
            return reuniao;
        }
    }
}
