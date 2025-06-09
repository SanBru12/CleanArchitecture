using Domain.Exceptions;
using Infrastructure.Exceptions;
using Infrastructure.Persistence.Context;
using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.DependencyInjection;
using Models.Models;
using Models.Responses;
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
            var response = new MultipleErrorResponse(exception.StatusCode, exception.InternalError, exception.Messages);
            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;

            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleErrorResponseExceptionAsync(HttpContext context, ErrorResponseException exception)
        {
            var response = new ErrorResponse(exception.StatusCode, exception.InternalError, exception.Message);

            context.Response.ContentType = "application/json";
            context.Response.StatusCode = exception.StatusCode;
            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }

        private async Task HandleExceptionAsync(HttpContext context, Exception exception)
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

            var response = new ErrorResponse(500, exception.Message, "Ocurrio un error interno en el servidor.");
            var result = JsonSerializer.Serialize(response);
            await context.Response.WriteAsync(result);
        }
    }
}

