using Domain.Entities;
using Infrastructure.Persistence.Context;
using Shared.Exceptions;
using Shared.Responses;
using System.Net;
using System.Text.Json;

namespace Infrastructure.Middleware
{
    public class ExceptionMiddleware(RequestDelegate next)
    {
        private readonly RequestDelegate _next = next;

        public async Task Invoke(HttpContext context)
        {
            try
            {
                await _next(context);
            }
            catch (AppException ex)
            {
                await HandleAppExceptionAsync(context, ex);
            }
            catch (Exception ex)
            {
                await HandleExceptionAsync(context, ex);
            }
        }

        private async Task HandleAppExceptionAsync(HttpContext context, AppException exception)
        {
            var response = ApiErrorResponse.FromException(exception);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            await context.Response.WriteAsync(JsonSerializer.Serialize(response));
        }

        private static async Task HandleExceptionAsync(HttpContext context, Exception exception)
        {
            using var scope = context.RequestServices.CreateScope();
            try
            {
                var dbContext = scope.ServiceProvider.GetRequiredService<ApplicationDbContext>();

                ErrorLog errorLog = new()
                {
                    Type = exception.GetType().ToString(),
                    Message = exception.Message,
                    StackTrace = exception.StackTrace,
                    Source = exception.Source,
                    TargetSite = exception.TargetSite?.ToString(),
                    InnerException = exception.InnerException?.ToString(),
                    CreateDate = DateTime.UtcNow,
                    Path = context.Request.Path,
                    Method = context.Request.Method
                };

                await dbContext.AddAsync(errorLog);
                await dbContext.SaveChangesAsync();
            }
            catch (Exception ex)
            {
                Console.WriteLine($"Error logging exception: {ex.Message}");
            }

            var responseContext = context.Response;
            responseContext.ContentType = "application/json";
            responseContext.StatusCode = (int)HttpStatusCode.InternalServerError;

            var response = new ApiErrorResponse(500, exception.Message, "Ocurrio un error interno en el servidor.");
            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
}

