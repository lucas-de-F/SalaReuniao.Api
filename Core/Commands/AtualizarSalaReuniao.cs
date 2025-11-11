using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core.Commands
{
    public class AtualizarSalaReuniaoCommand
    {
        public Guid Id { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public Endereco? Endereco { get; set; }
        public decimal ValorHora { get; set; }
    }
}