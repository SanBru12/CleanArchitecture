using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Logging;
using Models.Responses;
using System.Net;
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
            catch (MultipleErrorResponseException ex)
            {
                await HandleMultipleErrorResponseExceptionAsync(context, ex);
            }
            catch (ErrorResponseException ex)
            {
                await HandleErrorResponseExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleMultipleErrorResponseExceptionAsync(HttpContext context, MultipleErrorResponseException exception)
        {
            _logger.LogError("❌ [{Time}] {Method} {Path} | {Exception}: {Message}", DateTime.UtcNow, context.Request.Method, context.Request.Path, exception.GetType().Name, exception.Message);
            
            var response = new MultipleErrorResponse(exception.StatusCode, exception.InternalError, exception.Messages);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleErrorResponseExceptionAsync(HttpContext context, ErrorResponseException exception)
        {
            _logger.LogError("❌ [{Time}] {Method} {Path} | {Exception}: {Message}", DateTime.UtcNow, context.Request.Method, context.Request.Path, exception.GetType().Name, exception.Message);

            var response = new ErrorResponse(exception.StatusCode, exception.InternalError, exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            _logger.LogError("❌ [{Time}] {Method} {Path} | {Exception}: {Message}", DateTime.UtcNow, context.Request.Method, context.Request.Path, exception.GetType().Name, exception.Message);

            var responseContext = context.Response;
            responseContext.ContentType = "application/json";

            var statusCode = HttpStatusCode.InternalServerError;

            var response = new ErrorResponse((int)statusCode, exception.Message, "Ocurrio un error interno en el servidor, por favor intente nuevamente.");

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);

        }
    }
}
