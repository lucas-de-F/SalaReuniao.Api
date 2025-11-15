using System;
using System.Collections.Generic;
using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Api.Infrastructure.Entities
{
    public class EnderecoEntity
    {
        public string Rua { get; set; } = string.Empty;
        public int Numero { get; set; }
        public string Complemento { get; set; } = string.Empty;
        public string Bairro { get; set; } = string.Empty;
        public string Municipio { get; set; } = string.Empty;
        public string Estado { get; set; } = string.Empty;
        public string CEP { get; set; } = string.Empty;
    }
}
