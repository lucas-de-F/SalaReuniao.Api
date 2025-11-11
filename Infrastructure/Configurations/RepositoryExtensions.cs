using SalaReuniao.Api.Infrastructure.Repositories;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISalaDeReuniaoRepository, SalaDeReuniaoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();

            return services;
        }
    }
}
