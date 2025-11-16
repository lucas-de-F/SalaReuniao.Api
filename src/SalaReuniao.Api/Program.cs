using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Api.Configurations;
using SalaReuniao.Api.Extensions;

var builder = WebApplication.CreateBuilder(args);

// CORS sempre registrado
builder.Services.AddAppCors(builder.Configuration);

// Controllers
builder.Services
    .AddControllers()
    .AddJsonOptions(opt =>
    {
        opt.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
    });

builder.Services.Configure<ApiBehaviorOptions>(options =>
{
    options.InvalidModelStateResponseFactory = context =>
    {
        var errors = context.ModelState
            .Where(ms => ms.Value.Errors.Count > 0 && ms.Key != "command") // remover command
            .Select(ms => new {
                campo = ms.Key,
                mensagem = ms.Value.Errors.First().ErrorMessage
            })
            .ToList();

        var response = new
        {
            status = 400,
            message = "Erro ao mapear campos",
            errors = errors.Select(e => $"Erro ao mapear {e.campo}: {e.mensagem}")
        };

        return new BadRequestObjectResult(response);
    };
});

builder.Services.AddEndpointsApiExplorer();

// Configurações separadas
builder.Services.AddJwtAuthentication(builder.Configuration);
builder.Services.AddSwaggerWithJwt();
builder.Services.AddDatabase(builder.Configuration);
builder.Services.RegisterApplicationServices();

var app = builder.Build();

// Segurança (somente produção)
app.UseHstsIfProduction(app.Environment);
app.UseSecurityHeaders(app.Environment);
app.UseMiddleware<ErrorHandlingMiddleware>();

// Swagger DEV
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// ❗ CORS deve vir ANTES de tudo relacionado a auth
app.UseAppCors();

// ❗ HTTPS redirection só em produção para não atrapalhar dev
if (!app.Environment.IsDevelopment())
{
    app.UseHttpsRedirection();
}

app.UseAuthentication();
app.UseAuthorization();

app.MapControllers();

app.Run();
