namespace Kioskitos.API.Middleware;

public sealed class ExceptionHandlingMiddleware(
    RequestDelegate next,
    ILogger<ExceptionHandlingMiddleware> logger)
{
    public async Task InvokeAsync(HttpContext context)
    {
        try
        {
            await next(context);
        }
        catch (Exception exception)
            when (!context.Response.HasStarted
                  && !context.RequestAborted.IsCancellationRequested)
        {
            logger.LogError(
                exception,
                "Error inesperado. TraceId: {TraceId}",
                context.TraceIdentifier);

            context.Response.Clear();

            await Results.Problem(
                statusCode: StatusCodes.Status500InternalServerError,
                type: "about:blank",
                title: "Error interno del servidor",
                detail: "Ocurrió un error inesperado. Intentá nuevamente más tarde.",
                instance: context.Request.Path.Value,
                extensions: new Dictionary<string, object?>
                {
                    ["traceId"] = context.TraceIdentifier
                })
                .ExecuteAsync(context);
        }
    }
}