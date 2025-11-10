using System;

namespace SalaReuniao.Api.Core
{
    public class Servico
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public string Descricao { get; set; } = string.Empty;
        public string Unidade { get; set; } = string.Empty;
        public string Categoria { get; set; } = string.Empty;
        public Guid IdResponsavel { get; set; }

        public Responsavel Responsavel { get; set; } = null!;
        public ICollection<SalaServicoOferecido> ServicosOferecidos { get; set; } = new List<SalaServicoOferecido>();
    }
}
