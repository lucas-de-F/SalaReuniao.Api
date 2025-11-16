using Microsoft.Extensions.DependencyInjection;
using SalaReuniao.Api.Extensions;
using SalaReuniao.Api.Infrastructure.Mappings;

namespace SalaReuniao.Api.Configurations
{
    public static class ServicesConfig
    {
        public static IServiceCollection RegisterApplicationServices(this IServiceCollection services)
        {
            services.AddSalaReuniaoHandlers();
            services.AddRepositories();
            services.AddAutoMapper(typeof(DomainProfile).Assembly);

            return services;
        }
    }
}
