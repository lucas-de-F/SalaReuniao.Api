using System.Text;
using System.Text.Json.Serialization;
using Microsoft.AspNetCore.Authentication.JwtBearer;
using Microsoft.EntityFrameworkCore;
using Microsoft.IdentityModel.Tokens;
using Microsoft.OpenApi.Models;
using SalaReuniao.Api.Extensions;
using SalaReuniao.Api.Infrastructure;
using SalaReuniao.Api.Infrastructure.Mappings;

var builder = WebApplication.CreateBuilder(args);

// chave secreta forte
var secretKey = builder.Configuration["Jwt:Key"] ?? "MinhaChaveUltraSecreta123!";

// 🔐 JWT Authentication (REGISTRA APENAS UMA VEZ)
builder.Services
    .AddAuthentication(JwtBearerDefaults.AuthenticationScheme)
    .AddJwtBearer(options =>
    {
        options.TokenValidationParameters = new TokenValidationParameters
        {
            ValidateIssuer = false,
            ValidateAudience = false,
            ValidateLifetime = true,
            ValidateIssuerSigningKey = true,
            IssuerSigningKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(secretKey)),
            ClockSkew = TimeSpan.Zero
        };
    });

builder.Services.AddAuthorization();

// Controllers
builder.Services.AddControllers().AddJsonOptions(options =>
{
    options.JsonSerializerOptions.Converters.Add(new JsonStringEnumConverter());
});

builder.Services.AddEndpointsApiExplorer();

// 📘 Swagger com JWT (REGISTRA APENAS UMA VEZ)
builder.Services.AddSwaggerGen(c =>
{
    c.SwaggerDoc("v1", new() { Title = "SalaReuniao API", Version = "v1" });

    c.AddSecurityDefinition("Bearer", new OpenApiSecurityScheme
    {
        Description = "Insira o token JWT assim: Bearer {seu token}",
        Name = "Authorization",
        In = ParameterLocation.Header,
        Type = SecuritySchemeType.ApiKey,
        Scheme = "Bearer"
    });

    c.AddSecurityRequirement(new OpenApiSecurityRequirement
    {
        {
            new OpenApiSecurityScheme
            {
                Reference = new OpenApiReference
                {
                    Type = ReferenceType.SecurityScheme,
                    Id = "Bearer"
                }
            },
            Array.Empty<string>()
        }
    });
});

// Repositórios e Handlers
builder.Services.AddSalaReuniaoHandlers();
builder.Services.AddRepositories();

// DB
builder.Services.AddDbContext<AppDbContext>(options =>
    options.UseNpgsql(builder.Configuration.GetConnectionString("DefaultConnection"))
);

// AutoMapper
builder.Services.AddAutoMapper(typeof(DomainProfile).Assembly);

var app = builder.Build();

// Swagger
if (app.Environment.IsDevelopment())
{
    app.UseSwagger();
    app.UseSwaggerUI();
}

// Middlewares
app.UseHttpsRedirection();
app.UseMiddleware<ErrorHandlingMiddleware>();

// 🔐 Ativa JWT
app.UseAuthentication();
app.UseAuthorization();

// Endpoints
app.MapControllers();

app.Run();
