using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core.Dtos
{
    public class SalaDeReuniaoDetalhadaResult
    {
        public Guid Id { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public Guid IdResponsavel { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public EnderecoResult Endereco { get; set; } = null!;
        public decimal ValorHora { get; set; }
        public ResponsavelResult? Responsavel { get; set; } = null!;
        public List<Disponibilidade> Disponibilidades { get; set; } = new();
        public List<ReuniaoAgendadaSimpleResult> ReunioesAgendadas { get; set; } = new();
    }
}
