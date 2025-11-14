using SalaReuniao.Domain.ValueObjects;

namespace SalaReuniao.Domain.Services
{
    public interface IEnderecoService
    {
        Task<DadosEndereco> ConsultarCepAsync(string cep);
    }
}
