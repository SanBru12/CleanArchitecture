using Microsoft.AspNetCore.Http;
using Models.Responses;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    public class ExceptionMiddleware
    {
        private readonly RequestDelegate _next;

        public ExceptionMiddleware(RequestDelegate next)
        {
            _next = next;
        }

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            var statusCode = (int)HttpStatusCode.InternalServerError;
            var response = new ErrorResponse(statusCode, exception.Message, "Ocurrió un error interno en el servidor.");

            // Capturar excepciones específicas y personalizar el mensaje
            if (exception is ArgumentException)
            {
                statusCode = (int)HttpStatusCode.BadRequest;
                response = new ErrorResponse(statusCode, exception.Message, exception.InnerException?.Message ?? "");
            }

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = statusCode;

            var result = JsonSerializer.Serialize(response);

            await context.Response.WriteAsync(result);
        }
    }
}
