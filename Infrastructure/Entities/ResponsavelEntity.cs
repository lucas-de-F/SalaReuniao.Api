using System.Collections.Generic;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ResponsavelEntity : UsuarioEntity
    {
        public ICollection<SalaDeReuniaoEntity> Salas { get; set; } = new List<SalaDeReuniaoEntity>();
        public ICollection<ServicoEntity> ServicosCadastrados { get; set; } = new List<ServicoEntity>();
    }
}