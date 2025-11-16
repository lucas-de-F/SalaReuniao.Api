using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core.Commands
{
    public class CriarSalaReuniaoCommand
    {
        public Guid IdResponsavel { get; set; }
        public string Nome { get; set; } = string.Empty;
        public int Capacidade { get; set; }
        public string Descricao { get; set; } = string.Empty;
        public CriarEnderecoCommand Endereco { get; set; }
        public decimal ValorHora { get; set; }
        public DisponibilidadeSemanal DisponibilidadeSemanal { get; set; }
    }
}