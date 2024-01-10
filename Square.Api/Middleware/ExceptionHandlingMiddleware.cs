using System.Net;
using System.Text.Json;

namespace Square.Api.Middleware
{
    public class ExceptionHandlingMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionHandlingMiddleware> _logger;

        public ExceptionHandlingMiddleware(RequestDelegate next, ILogger<ExceptionHandlingMiddleware> logger)
        {
            _next = next ?? throw new ArgumentNullException(nameof(next));
            _logger = logger ?? throw new ArgumentNullException(nameof(logger));
        }

        public async Task InvokeAsync(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                _logger.LogError(ex, $"An unhandled exception occurred: {ex.Message}");
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = (int)HttpStatusCode.InternalServerError;

            var errorResponse = new
            {
                Message = "An unexpected error occurred.",
                ExceptionType = exception.GetType().Name,
                StatusCode = context.Response.StatusCode,
                Timestamp = DateTime.UtcNow,
                Details = GetExceptionDetails(exception)
            };

            _logger.LogError($"Error details: {JsonSerializer.Serialize(errorResponse)}");

            await context.Response.WriteAsync(JsonSerializer.Serialize(errorResponse));
        }

        private string GetExceptionDetails(Exception exception)
        {
            var exceptionDetails = new
            {
                Message = exception.Message,
                Source = exception.Source,
                StackTrace = exception.StackTrace,
                InnerException = exception.InnerException != null ? GetExceptionDetails(exception.InnerException) : null
            };

            return JsonSerializer.Serialize(exceptionDetails);
        }
    }
}
