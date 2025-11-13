using System;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class Disponibilidade
    {
        public DayOfWeek DiaSemana { get; private set; }
        public TimeSpan Inicio { get; private set; }
        public TimeSpan Fim { get; private set; }

        private Disponibilidade() { }

        public Disponibilidade(DayOfWeek diaSemana, TimeSpan inicio, TimeSpan fim)
        {
            if (inicio >= fim)
                throw new DomainException("O horário de início deve ser anterior ao horário de fim.");

            DiaSemana = diaSemana;
            Inicio = inicio;
            Fim = fim;
        }

        /// <summary>
        /// Retorna true se o horário solicitado está dentro da janela de disponibilidade.
        /// </summary>
        public bool EstaDentroDoHorario(DateTime inicio, DateTime fim)
        {
            if (inicio.DayOfWeek != DiaSemana)
                return false;

            var horaInicio = inicio.TimeOfDay;
            var horaFim = fim.TimeOfDay;

            return horaInicio >= Inicio && horaFim <= Fim;
        }

        public override string ToString()
        {
            return $"{DiaSemana}: {Inicio:hh\\:mm} - {Fim:hh\\:mm}";
        }
    }
}
