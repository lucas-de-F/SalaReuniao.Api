using System.Text.Json.Serialization;
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
