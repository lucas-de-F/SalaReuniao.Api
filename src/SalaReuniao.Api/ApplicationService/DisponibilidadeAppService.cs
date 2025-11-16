using SalaReuniao.Api.Core;
using SalaReuniao.Api.Infrastructure.Entities;
using SalaReuniao.Domain.Repositories;

public class DisponibilidadeAppService
{
    private readonly IDisponibilidadeRepository _disponibilidadeRepository;

    public DisponibilidadeAppService(IDisponibilidadeRepository disponibilidadeRepository)
    {
        _disponibilidadeRepository = disponibilidadeRepository;
    }

    public async Task AtualizarDisponibilidadesAsync(SalaDeReuniao sala)
    {
        // Remove todas as disponibilidades antigas
        await _disponibilidadeRepository.RemoverPorSalaReuniaoIdAsync(sala.Id);

        // Adiciona as novas disponibilidades da própria entidade
        foreach (var disp in sala.DisponibilidadeSemanal.Disponibilidades)
        {
            await _disponibilidadeRepository.AdicionarAsync(new DisponibilidadeEntity
            {
                Id = Guid.NewGuid(),
                SalaDeReuniaoId = sala.Id,
                DiaSemana = disp.DiaSemana,
                Inicio = disp.Inicio,
                Fim = disp.Fim
            });
        }

        await _disponibilidadeRepository.SalvarAlteracoesAsync();
    }
}
