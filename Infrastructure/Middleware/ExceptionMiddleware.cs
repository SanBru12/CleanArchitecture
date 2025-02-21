using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.Exceptions;
using Models.Responses;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;
        private readonly ILogger<ExceptionMiddleware> _logger;

        public ExceptionMiddleware(RequestDelegate next, ILogger<ExceptionMiddleware> logger)
        {
            _next = next;
            _logger = logger;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (ErrorResponseException ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleExceptionAsync(HttpContext context, ErrorResponseException exception)
        {
            _logger.LogError("❌ [{Time}] {Method} {Path} | {Exception}: {Message}", DateTime.UtcNow, context.Request.Method, context.Request.Path, exception.GetType().Name, exception.Message);
            
            var response = new ErrorResponse(exception.StatusCode, exception.InternalError, exception.Messages);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
}
