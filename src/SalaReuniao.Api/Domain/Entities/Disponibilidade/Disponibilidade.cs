using System;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class Disponibilidade
    {
        public DayOfWeek DiaSemana { get; private set; }
        public TimeOnly Inicio { get; private set; }
        public TimeOnly Fim { get; private set; }

        private Disponibilidade() { }

        public Disponibilidade(DayOfWeek diaSemana, TimeOnly inicio, TimeOnly fim)
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
        public bool EstaDentroDoHorario(DateOnly data, TimeOnly inicio, TimeOnly fim)
        {
            if (data.DayOfWeek != DiaSemana)
                return false;

            return inicio >= Inicio && fim <= Fim;
        }

        public override string ToString()
        {
            return $"{DiaSemana}: {Inicio:hh\\:mm} - {Fim:hh\\:mm}";
        }
    }
}
