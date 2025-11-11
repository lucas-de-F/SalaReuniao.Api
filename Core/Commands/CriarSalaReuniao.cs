using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core.Commands
{
    public class CriarSalaReuniaoCommand
    {
        public Guid IdResponsavel { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Endereco Endereco { get; set; } = new Endereco("", "", "", "", "", "");
        public decimal ValorHora { get; set; }
    }
}