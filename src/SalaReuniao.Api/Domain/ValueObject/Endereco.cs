using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class Endereco
    {
        public string Rua { get; private set; }
        public int Numero { get; private set; }
        public string Bairro { get; private set; }
        public string Municipio { get; private set; }
        public string Cidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }

        // Construtor para criar Endereço
        public Endereco(string rua, int numero, string bairro, string cidade, string estado, string cep)
        {
            if (string.IsNullOrWhiteSpace(rua)) throw new DomainException("Rua é obrigatória.");
            if (string.IsNullOrWhiteSpace(cidade)) throw new DomainException("Cidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(estado)) throw new DomainException("Estado é obrigatório.");
            
            Rua = rua.Trim();
            Numero = numero;
            Bairro = bairro.Trim();
            Cidade = cidade.Trim();
            Estado = estado.Trim();
            CEP = cep.Trim();
        }

        // Necessário para EF / serialização JSON
        private Endereco() { }

        public void Atualizar(Endereco novoEndereco)
        {
            if (novoEndereco == null) return;

            Rua = novoEndereco.Rua;
            Numero = novoEndereco.Numero;
            Bairro = novoEndereco.Bairro;
            Cidade = novoEndereco.Cidade;
            Estado = novoEndereco.Estado;
            Municipio = novoEndereco.Municipio;
            CEP = novoEndereco.CEP;
        }
    }
}