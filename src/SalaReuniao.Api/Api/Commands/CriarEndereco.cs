using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core.Commands
{
    public class CriarEnderecoCommand
    {
        public int Numero { get;  set; }
        public string Complemento { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
    }
}