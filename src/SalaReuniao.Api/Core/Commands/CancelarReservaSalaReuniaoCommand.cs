
namespace SalaReuniao.Api.Core.Commands
{
    public class CancelarReservaSalaReuniaoCommand
    {
        public Guid IdSala { get; set; }
        public Guid IdReserva { get; set; }
    }
}