using System.Collections.Generic;
using System.Linq;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class DisponibilidadeSemanal
    {
        public List<Disponibilidade> Disponibilidades { get; set; } = new();

        public DisponibilidadeSemanal() { }

        public DisponibilidadeSemanal(IEnumerable<Disponibilidade> disponibilidades)
        {
            Validar(disponibilidades);
            Disponibilidades = disponibilidades.ToList();
        }

        private void Validar(IEnumerable<Disponibilidade> disponibilidades)
        {
            if (disponibilidades == null || !disponibilidades.Any())
                throw new DomainException("A disponibilidade semanal deve conter pelo menos um dia.");

            var duplicados = disponibilidades
                .GroupBy(d => d.DiaSemana)
                .Where(g => g.Count() > 1)
                .Select(g => g.Key)
                .ToList();

            if (duplicados.Any())
                throw new DomainException($"Há dias duplicados na disponibilidade: {string.Join(", ", duplicados)}");

            if (disponibilidades.Count() > 7)
                throw new DomainException("A disponibilidade semanal não pode ter mais que 7 dias.");
        }

        public bool EstaDisponivel(DateOnly data, TimeOnly inicio, TimeOnly fim)
        {
            var dia = data.DayOfWeek;
            var disponibilidade = Disponibilidades.FirstOrDefault(d => d.DiaSemana == dia);
            return disponibilidade != null && disponibilidade.EstaDentroDoHorario(data, inicio, fim);
        }

        public static DisponibilidadeSemanal Padrao()
        {
            var horarios = Enumerable.Range(1, 5).Select(i =>
                new Disponibilidade((DayOfWeek)i, new TimeOnly(8, 0), new TimeOnly(18, 0))
            );
            return new DisponibilidadeSemanal(horarios);
        }

         public static DisponibilidadeSemanal? FromEntities(ICollection<DisponibilidadeEntity> entities)
     {
        if (entities == null || !entities.Any())
            return null;

        // Aqui você pode implementar a lógica que faz sentido no seu domínio
        // Por exemplo, criar um objeto consolidado da semana inteira
        // Vou supor que você só queira pegar a primeira para simplificar

        return new DisponibilidadeSemanal
        {
           Disponibilidades = entities.Select(e => new Disponibilidade(e.DiaSemana, e.Inicio, e.Fim)).ToList()
        };
    }
}
}
