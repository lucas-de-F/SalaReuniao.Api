using Microsoft.AspNetCore.Builder;

namespace SalaReuniao.Api.Configurations
{
    public static class HstsConfig
    {
        public static IApplicationBuilder UseHstsIfProduction(
            this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsProduction())
            {
                app.UseHsts();
            }

            return app;
        }
    }
}
