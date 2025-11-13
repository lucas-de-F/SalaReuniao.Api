using System;
using System.Collections.Generic;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class SalaDeReuniaoEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Endereco Endereco { get; set; } = null!;
        public decimal ValorHora { get; set; }
        public UsuarioEntity Responsavel { get; set; } = null!;
        public ICollection<ReuniaoAgendadaEntity> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendadaEntity>();
        public ICollection<SalaServicoOferecidoEntity> ServicosOferecidos { get; set; } = new List<SalaServicoOferecidoEntity>();
        public ICollection<DisponibilidadeEntity> Disponibilidades { get; set; } = new List<DisponibilidadeEntity>();
    }
}
