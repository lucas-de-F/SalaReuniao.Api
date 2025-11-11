using SalaReuniao.Api.Core;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSalaReuniaoHandlers(this IServiceCollection services)
    {
        services.AddScoped<CriarSalaReuniaoHandler>();
        services.AddScoped<ListarSalasReuniaoHandler>();
        services.AddScoped<AtualizarSalaReuniaoHandler>();
        services.AddScoped<RemoverSalaDeReuniaoHandler>();

        return services;
    }
}
