using System.Net.Http.Json;
using System.Text.Json;
using SalaReuniao.Api.Infrastructure.Exceptions;
using SalaReuniao.Domain.Services;
using SalaReuniao.Domain.ValueObject;
using SalaReuniao.Domain.ValueObjects;

public class EnderecoService : IEnderecoService
{
    private readonly HttpClient _http;

    public EnderecoService(HttpClient http)
    {
        _http = http;
    }

    public async Task<DadosEndereco> ConsultarCepAsync(string cep)
    {
        try
        {
            var result = await _http.GetFromJsonAsync<ViaCepResponse>(
                $"https://viacep.com.br/ws/{cep}/json/"
            );

            if (result == null || string.IsNullOrEmpty(result.Logradouro))
                throw new InfrastructureException("CEP inválido ou não encontrado.");
                
            var enderecoCompleto = new DadosEndereco
            {
                Rua = result.Logradouro,
                Bairro = result.Bairro,
                Localidade = result.Localidade,
                CEP = cep,
                Estado = result.Uf
            };
               

            return enderecoCompleto;
        }
        catch (Exception E)
        {
            if (E is HttpRequestException || E is NotSupportedException || E is JsonException)
            throw new InfrastructureException("Falha ao consultar o CEP.");
            throw;
        }

    }

}

public class ViaCepResponse
{
    public string Logradouro { get; set; } = string.Empty; // Rua, Avenida, Praça etc
    public string Bairro { get; set; } = string.Empty;
    public string Localidade { get; set; } = string.Empty; // Municipio
    public string Uf { get; set; } = string.Empty; // Estado
}