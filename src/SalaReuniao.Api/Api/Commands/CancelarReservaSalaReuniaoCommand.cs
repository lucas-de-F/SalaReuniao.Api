
namespace SalaReuniao.Api.Core.Commands
{
    public class CancelarReservaSalaReuniaoCommand
    {
        public Guid UserId { get; set; }
        public Guid IdReserva { get; set; }
    }
}