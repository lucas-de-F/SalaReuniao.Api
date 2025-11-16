namespace SalaReuniao.Api.Core.Commands
{
    public class ReservaSalaReuniaoCommand
    {
        public Guid IdCliente { get; set; }
        public Guid IdSala { get; set; }
        public DateOnly Data { get; set; }
        public TimeOnly Inicio { get; set; }
        public TimeOnly Fim { get; set; }
    }
}

