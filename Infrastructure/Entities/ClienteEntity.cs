namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class ClienteEntity : UsuarioEntity
    {
        public ICollection<ReuniaoAgendadaEntity> ReunioesAgendadas { get; set; } = new List<ReuniaoAgendadaEntity>();
    }
}