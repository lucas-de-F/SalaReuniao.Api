using System.Net;
using System.Text.Json;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using SalaReuniao.Domain.Exceptions;

public class ErrorHandlingMiddleware
{
    private readonly RequestDelegate _next;

    public ErrorHandlingMiddleware(RequestDelegate next)
    {
        _next = next;
    }

    public async Task Invoke(HttpContext context)
    {
        try
        {
            // Processa a requisição normalmente
            await _next(context);

            // Intercepta respostas 400 geradas pelo model binding / ModelState
            if (context.Response.StatusCode == StatusCodes.Status400BadRequest && !context.Response.HasStarted)
            {
                var problemDetails = new ValidationProblemDetails(new Microsoft.AspNetCore.Mvc.ModelBinding.ModelStateDictionary())
                {
                    Title = "Um ou mais erros de validação ocorreram.",
                    Status = StatusCodes.Status400BadRequest,
                };

                context.Response.ContentType = "application/problem+json";
                var json = JsonSerializer.Serialize(problemDetails);
                await context.Response.WriteAsync(json);
            }
        }
        catch (DomainException ex)
        {
            await HandleValidationExceptionAsync(context, ex);
        }
        catch (Exception ex)
        {
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleValidationExceptionAsync(HttpContext context, DomainException ex)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status400BadRequest;

        var problemDetails = new ValidationProblemDetails()
        {
            Title = "Um ou mais erros de validação ocorreram.",
            Status = StatusCodes.Status400BadRequest,
        };

        problemDetails.Errors.Add("Domain", new[] { ex.Message });

        var json = JsonSerializer.Serialize(problemDetails);
        return context.Response.WriteAsync(json);
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception ex)
    {
        context.Response.ContentType = "application/problem+json";
        context.Response.StatusCode = StatusCodes.Status500InternalServerError;

        var problemDetails = new ProblemDetails
        {
            Title = "Ocorreu um erro inesperado.",
            Status = StatusCodes.Status500InternalServerError,
            Detail = ex.Message,
        };

        var json = JsonSerializer.Serialize(problemDetails);
        return context.Response.WriteAsync(json);
    }
}
