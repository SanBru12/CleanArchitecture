using Shared.Exceptions;

namespace Shared.Responses
{
    public class ApiErrorResponse(int statusCode, string internalError, string? message = null, List<string>? messages = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string InternalError { get; set; } = internalError;
        public string? Message { get; set; } = message;
        public List<string>? Messages { get; set; } = messages?.Count > 0 ? messages : null;

        // Conveniencia para errores múltiples
        public static ApiErrorResponse FromException(AppException ex) =>
             new(ex.StatusCode, ex.InternalError, ex.Message, ex.Messages);

        public static ApiErrorResponse FromGeneric(Exception ex) =>
            new(500, ex.GetType().Name, ex.Message);
    }
}