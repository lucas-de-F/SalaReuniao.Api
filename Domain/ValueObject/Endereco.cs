namespace SalaReuniao.Domain.ValueObjects
{
    public class Endereco
    {
        public string Rua { get; private set; } = string.Empty;
        public string Numero { get; private set; } = string.Empty;
        public string Bairro { get; private set; } = string.Empty;
        public string Cidade { get; private set; } = string.Empty;
        public string Estado { get; private set; } = string.Empty;
        public string CEP { get; private set; } = string.Empty;

        // Construtor para criar Endereço
        public Endereco(string rua, string numero, string bairro, string cidade, string estado, string cep)
        {
            if (string.IsNullOrWhiteSpace(rua)) throw new ArgumentException("Rua é obrigatória.");
            if (string.IsNullOrWhiteSpace(cidade)) throw new ArgumentException("Cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(estado)) throw new ArgumentException("Estado é obrigatório.");
            
            Rua = rua.Trim();
            Numero = numero.Trim();
            Bairro = bairro.Trim();
            Cidade = cidade.Trim();
            Estado = estado.Trim();
            CEP = cep.Trim();
        }

        // Necessário para EF / serialização JSON
        private Endereco() { }
    }
}