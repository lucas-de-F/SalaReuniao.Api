using SalaReuniao.Api.Infrastructure.Entities;

namespace SalaReuniao.Domain.Repositories
{
    public interface IDisponibilidadeRepository
    {
        Task<DisponibilidadeEntity?> ObterDisponibilidadeByIdAsync(Guid id);
        Task<DisponibilidadeEntity?> ObterDisponibilidadeBySalaReuniaoIdAsync(Guid id);
        Task AdicionarAsync(DisponibilidadeEntity disponibilidade);
        Task<ICollection<DisponibilidadeEntity>> ObterDisponibilidadesAsync();
        Task SalvarAlteracoesAsync();
    }
}
