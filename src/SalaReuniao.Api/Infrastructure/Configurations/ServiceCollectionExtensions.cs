using SalaReuniao.Api.Core;
using SalaReuniao.Domain.Services;

public static class ServiceCollectionExtensions
{
    public static IServiceCollection AddSalaReuniaoHandlers(this IServiceCollection services)
    {
        services.AddScoped<CriarSalaReuniaoHandler>();
        services.AddScoped<ListarSalasReuniaoHandler>();
        services.AddScoped<AtualizarSalaReuniaoHandler>();
        services.AddScoped<RemoverSalaDeReuniaoHandler>();
        services.AddScoped<IEnderecoService, EnderecoService>();
        return services;
    }
}
