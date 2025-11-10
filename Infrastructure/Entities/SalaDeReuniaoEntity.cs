using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class SalaDeReuniaoEntity
    {
        public Guid Id { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Guid IdEndereco { get; set; }
        public decimal ValorHora { get; set; }

        public ResponsavelEntity Responsavel { get; set; } = null!;
        public ICollection<ReuniaoAgendadaEntity> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendadaEntity>();
        public ICollection<SalaServicoOferecidoEntity> ServicosOferecidos { get; set; } = new List<SalaServicoOferecidoEntity>();
    }
}
