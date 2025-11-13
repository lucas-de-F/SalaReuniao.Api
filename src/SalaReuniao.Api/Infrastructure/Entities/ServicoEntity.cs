using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ServicoEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Unidade { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public Guid IdResponsavel { get; set; }
        public UsuarioEntity Responsavel { get; set; } = null!;
        public ICollection<SalaServicoOferecidoEntity> ServicosOferecidos { get; set; } = new List<SalaServicoOferecidoEntity>();
    }
}
