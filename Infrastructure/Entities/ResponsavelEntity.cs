using System;
using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ResponsavelEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<SalaDeReuniaoEntity> Salas { get; set; } = new List<SalaDeReuniaoEntity>();
        public ICollection<ServicoEntity> ServicosCadastrados { get; set; } = new List<ServicoEntity>();
    }
}
