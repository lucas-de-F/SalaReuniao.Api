using Microsoft.Extensions.DependencyInjection;

namespace SalaReuniao.Api.Configurations
{
    public static class CorsConfig
    {
        private const string CorsPolicyName = "SecureCorsPolicy";

        public static IServiceCollection AddAppCors(this IServiceCollection services, IConfiguration config)
        {
    var frontUrls = config.GetSection("Frontend:Url").Get<string[]>();
            Console.WriteLine($"CORS configured to allow requests from: {string.Join(", ", frontUrls)}");
            services.AddCors(options =>
            {
                options.AddPolicy(CorsPolicyName, policy =>
                {
                    policy.WithOrigins(frontUrls)
                          .WithMethods("GET", "POST", "PUT", "PATCH", "DELETE")
                          .WithHeaders("Content-Type", "Authorization")
                          .AllowCredentials()
                          .WithExposedHeaders("Content-Disposition");
                });
            });

            return services;
        }

        public static IApplicationBuilder UseAppCors(this IApplicationBuilder app)
        {
            return app.UseCors(CorsPolicyName);
        }
    }
}
