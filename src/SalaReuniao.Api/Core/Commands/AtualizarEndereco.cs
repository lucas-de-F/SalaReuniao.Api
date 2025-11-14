using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Core.Commands
{
    public class AtualizarEndereco
    {
        public int Numero { get; private set; }
        public string Complemento { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
    }
}