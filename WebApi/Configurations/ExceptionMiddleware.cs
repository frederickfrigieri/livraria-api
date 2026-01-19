using Application.Exceptions;
using Domain.Exceptions;
using Infrastructure.Databases;

public class ExceptionMiddleware
{
    private readonly RequestDelegate _next;
    private readonly ILogger<ExceptionMiddleware> _logger;

    public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
    {
        _next = next;
        _logger = logger;
    }

    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await _next(context);
        }
        catch (Exception ex)
        {
            _logger.LogError(ex, "Erro capturado no Middleware");
            await HandleExceptionAsync(context, ex);
        }
    }

    private static Task HandleExceptionAsync(HttpContext context, Exception exception)
    {
        context.Response.ContentType = "application/json";

        var (StatusCode, Message, Errors) = exception switch
        {
            DomainException domainEx => (StatusCode: 422, domainEx.Message, Errors: null),
            RepositoryException dbEx => (StatusCode: 409, dbEx.Message, Errors: null),
            ValidationException valEx => (StatusCode: 400, Message: "Erros de validação encontrados", valEx.Errors),

            _ => (StatusCode: 500, Message: "Ocorreu um erro interno inesperado.", Errors: null)
        };

        context.Response.StatusCode = StatusCode;

        return context.Response.WriteAsJsonAsync(new
        {
            error = Message,
            statusCode = StatusCode,
            details = Errors 
        });
    }
}