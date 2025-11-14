using SalaReuniao.Domain.Exceptions;

namespace SalaReuniao.Domain.ValueObjects
{
    public class Endereco
    {
        public string Rua { get; private set; }
        public int Numero { get; private set; }
        public string Complemento { get; private set; }
        public string Bairro { get; private set; }
        public string Localidade { get; private set; }
        public string Estado { get; private set; }
        public string CEP { get; private set; }

        // Construtor para criar Endereço
        public Endereco(DadosEndereco dadosEndereco, DadosComplementaresEndereco dadosComplementaresEndereco)
        {
            ValidarDadosEndereco(dadosEndereco);

            AtribuiuDadosEndereco(dadosEndereco, dadosComplementaresEndereco);
        }

        // Necessário para EF / serialização JSON
        private Endereco() { }

        public void ValidarDadosEndereco(DadosEndereco? dadosEndereco)
        {
            if (dadosEndereco == null)
                throw new DomainException("Dados do endereço são obrigatórios.");
            if (string.IsNullOrWhiteSpace(dadosEndereco.CEP)) throw new DomainException("CEP é obrigatório.");
            if (string.IsNullOrWhiteSpace(dadosEndereco.Rua)) throw new DomainException("Rua é obrigatória.");
            if (string.IsNullOrWhiteSpace(dadosEndereco.Localidade)) throw new DomainException("Localidade é obrigatória.");
            if (string.IsNullOrWhiteSpace(dadosEndereco.Estado)) throw new DomainException("Estado é obrigatório.");
        }
        private void AtribuiuDadosEndereco(DadosEndereco? dadosEndereco, DadosComplementaresEndereco? dadosComplementaresEndereco)
        {
            if (dadosEndereco != null)
            {
                Rua = dadosEndereco.Rua ?? Rua;
                Bairro = dadosEndereco.Bairro ?? Bairro;
                Localidade = dadosEndereco.Localidade ?? Localidade;
                Estado = dadosEndereco.Estado ?? Estado;
                CEP = dadosEndereco.CEP ?? CEP;
            }
            if (dadosComplementaresEndereco != null)
            {
                Numero = dadosComplementaresEndereco.Numero;
                Complemento = dadosComplementaresEndereco.Complemento.Trim();
            }
        }
        public void Atualizar(DadosEndereco? dadosEndereco, DadosComplementaresEndereco? dadosComplementaresEndereco)
        {
            if (dadosEndereco != null) ValidarDadosEndereco(dadosEndereco);

            AtribuiuDadosEndereco(dadosEndereco, dadosComplementaresEndereco);
        }
    }
    public class DadosComplementaresEndereco
    {
        public int Numero { get; set; }
        public string Complemento { get; set; } = string.Empty;
    }
    public class DadosEndereco
    {
        public string CEP { get; set; } = string.Empty;
        public string Rua { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string Localidade { get; set; } = string.Empty;
    }
}
