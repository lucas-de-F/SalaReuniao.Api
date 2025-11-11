
namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class UsuarioEntity
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;

        public ICollection<ReuniaoAgendadaEntity> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendadaEntity>();
        public ICollection<SalaDeReuniaoEntity> Salas { get; set; } = new List<SalaDeReuniaoEntity>();
        public ICollection<ServicoEntity> ServicosCadastrados { get; set; } = new List<ServicoEntity>();

    }
}
