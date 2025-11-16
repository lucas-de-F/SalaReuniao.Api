using System;
using System.Collections.Generic;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Api.Core
{
    public class ReuniaoAgendada
    {
        public Guid Id { get; set; }
        public Guid IdSalaReuniao { get; set; }
        public Guid IdCliente { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
        public DateOnly Data { get; set; }
        public ReuniaoStatus Status { get; set; }

        public ReuniaoAgendada(Guid id, Guid idSalaReuniao, Guid idCliente, DateOnly data, TimeOnly inicio, TimeOnly fim)
        {
            Id = id;
            IdSalaReuniao = idSalaReuniao;
            IdCliente = idCliente;
            Data = data;
            Inicio = inicio;
            Fim = fim;
        }
        public decimal Duracao => (decimal)(Fim - Inicio).TotalHours;
        public void Cancelar()
        {
            if (Data < DateOnly.FromDateTime(DateTime.Now))
                throw new DomainException("Não é possível cancelar uma reunião que já ocorreu.");
            if  (Status == ReuniaoStatus.Cancelada)
                throw new DomainException("A reunião já está cancelada.");
            Status = ReuniaoStatus.Cancelada;
        }
        public ICollection<ServicoAgendado> ServicosAgendados { get; set; } = new List<ServicoAgendado>();
    }
}
