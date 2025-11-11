using System;
using System.Collections.Generic;
using System.Linq;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class DisponibilidadeSemanal
    {
        private readonly List<Disponibilidade> _disponibilidades = new();
        public IReadOnlyCollection<Disponibilidade> Disponibilidades => _disponibilidades.AsReadOnly();

        private DisponibilidadeSemanal() { }

        public DisponibilidadeSemanal(IEnumerable<Disponibilidade> disponibilidades)
        {
            if (disponibilidades == null || !disponibilidades.Any())
                throw new DomainException("A disponibilidade semanal deve conter pelo menos um dia.");

            // Garante que não haja duplicidade de dias
            var duplicados = disponibilidades
                .GroupBy(d => d.DiaSemana)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicados.Any())
                throw new DomainException($"Há dias duplicados na disponibilidade: {string.Join(", ", duplicados)}");

            // Garante que não há mais que 7 dias
            if (disponibilidades.Count() > 7)
                throw new DomainException("A disponibilidade semanal não pode ter mais que 7 dias.");

            _disponibilidades.AddRange(disponibilidades);
        }

        /// <summary>
        /// Verifica se a sala está disponível para o intervalo de tempo informado.
        /// </summary>
        public bool EstaDisponivel(DateTime inicio, DateTime fim)
        {
            var dia = inicio.DayOfWeek;
            var disponibilidade = _disponibilidades.FirstOrDefault(d => d.DiaSemana == dia);

            if (disponibilidade == null)
                return false; // sala fechada nesse dia

            return disponibilidade.EstaDentroDoHorario(inicio, fim);
        }

        /// <summary>
        /// Cria uma disponibilidade padrão (segunda a sexta das 8h às 18h)
        /// </summary>
        public static DisponibilidadeSemanal Padrao()
        {
            var horarios = Enumerable.Range(1, 5).Select(i =>
                new Disponibilidade((DayOfWeek)i, new TimeSpan(8, 0, 0), new TimeSpan(18, 0, 0))
            );

            return new DisponibilidadeSemanal(horarios);
        }
    }
}
