using Microsoft.AspNetCore.Builder;

namespace SalaReuniao.Api.Configurations
{
    public static class SecurityHeadersConfig
    {
        public static IApplicationBuilder UseSecurityHeaders(this IApplicationBuilder app, IWebHostEnvironment env)
        {
            if (env.IsDevelopment())
                return app; // 🔥 NÃO aplicar no desenvolvimento

            app.Use(async (context, next) =>
            {
                context.Response.Headers["Referrer-Policy"] = "strict-origin-when-cross-origin";
                context.Response.Headers["X-Frame-Options"] = "SAMEORIGIN";
                context.Response.Headers["X-Content-Type-Options"] = "nosniff";
                context.Response.Headers["X-XSS-Protection"] = "0";
                context.Response.Headers["X-Download-Options"] = "noopen";

                context.Response.Headers["Content-Security-Policy"] =
                    "default-src 'self'; " +
                    "script-src 'self'; " +
                    "object-src 'none'; " +
                    "frame-ancestors 'self'; " +
                    "base-uri 'self'; " +
                    "font-src 'self' https: data:; " +
                    "form-action 'self'; " +
                    "img-src 'self' data:; " +
                    "style-src 'self' https: 'unsafe-inline'; " +
                    "upgrade-insecure-requests";

                await next();
            });

            return app;
        }
    }
}
