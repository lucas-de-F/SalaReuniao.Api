using Microsoft.EntityFrameworkCore;
using SalaReuniao.Api.Infrastructure;

namespace SalaReuniao.Api.Configurations
{
    public static class DatabaseConfig
    {
        public static IServiceCollection AddDatabase(this IServiceCollection services, IConfiguration config)
        {
            services.AddDbContext<AppDbContext>(options =>
                options.UseNpgsql(config.GetConnectionString("DefaultConnection"))
            );

            return services;
        }
    }
}
