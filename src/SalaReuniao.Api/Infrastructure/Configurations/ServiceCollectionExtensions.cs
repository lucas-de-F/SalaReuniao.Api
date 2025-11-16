using SalaReuniao.Api.Core;
using SalaReuniao.Domain.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSalaReuniaoHandlers(this IServiceCollection services)
    {
        services.AddScoped<CriarSalaReuniaoHandler>();
        services.AddScoped<ObterSalaReuniaoHandler>();
        services.AddScoped<ListarSalasReuniaoHandler>();
        services.AddScoped<ListarAgendamentosHandler>();
        services.AddScoped<AtualizarSalaReuniaoHandler>();
        services.AddScoped<ObterFiltrosLocalidadeHandler>();
        services.AddScoped<RemoverSalaDeReuniaoHandler>();
        services.AddScoped<ReservarSalaReuniaoHandler>();
        services.AddScoped<LoginUsuarioHandler>();
        services.AddScoped<CancelarReservaSalaReuniaoHandler>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        services.AddHttpClient<IEnderecoService, EnderecoService>();

        // application services
        services.AddScoped<DisponibilidadeAppService>();
        services.AddScoped<SalaDeReuniaoAppService>();

        // domain services
        services.AddScoped<SalaDeReuniaoService>();

        return services;
    }
}
