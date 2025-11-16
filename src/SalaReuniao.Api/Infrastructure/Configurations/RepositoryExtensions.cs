using SalaReuniao.Api.Infrastructure.Repositories;
using SalaReuniao.Domain.Repositories;

namespace SalaReuniao.Api.Extensions
{
    public static class RepositoryExtensions
    {
        public static IServiceCollection AddRepositories(this IServiceCollection services)
        {
            services.AddScoped<ISalaDeReuniaoRepository, SalaDeReuniaoRepository>();
            services.AddScoped<IDisponibilidadeRepository, DisponibilidadeRepository>();
            services.AddScoped<IReservaSalaDeReuniaoRepository, ReservaSalaDeReuniaoRepository>();
            services.AddScoped<IUsuarioRepository, UsuarioRepository>();
            services.AddScoped<ILocalidadesSalaReuniaoRepository, LocalidadesSalaReuniaoRepository>();
            services.AddScoped<IAgendamentosRepository, AgendamentosRepository>();

            return services;
        }
    }
}
