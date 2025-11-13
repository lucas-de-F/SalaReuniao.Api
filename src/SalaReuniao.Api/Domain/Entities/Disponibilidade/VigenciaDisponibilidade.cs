using System;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class VigenciaDisponibilidade
    {
        public DateTime DataVigencia { get; private set; }
        public DisponibilidadeSemanal DisponibilidadeSemanal { get; private set; }
        public decimal ValorHora { get; private set; }

        private VigenciaDisponibilidade() { }

        public VigenciaDisponibilidade(DateTime dataVigencia, DisponibilidadeSemanal disponibilidadeSemanal, decimal valorHora)
        {
            if (dataVigencia == default)
                throw new DomainException("A data de vigência é obrigatória.");

            if (disponibilidadeSemanal == null)
                throw new DomainException("A disponibilidade semanal é obrigatória.");

            if (valorHora < 0)
                throw new DomainException("O valor por hora não pode ser negativo.");

            DataVigencia = dataVigencia;
            DisponibilidadeSemanal = disponibilidadeSemanal;
            ValorHora = valorHora;
        }

        public bool EstaDisponivel(DateTime inicio, DateTime fim)
        {
            if (inicio.Date < DataVigencia.Date)
                return false;

            return DisponibilidadeSemanal.EstaDisponivel(inicio, fim);
        }
    }
}
