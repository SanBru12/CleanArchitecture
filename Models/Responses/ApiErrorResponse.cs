namespace Shared.Responses
{
    public class ApiErrorResponse(int statusCode, string internalError, string? message = null, List<string>? messages = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string InternalError { get; set; } = internalError;
        public string? Message { get; set; } = message;
        public List<string>? Messages { get; set; } = messages?.Count > 0 ? messages : null;

        // Conveniencia para errores múltiples
        public ApiErrorResponse FromMultiple(int statusCode, string internalError, List<string> messages) =>
            new(statusCode, internalError, null, messages);

        // Conveniencia para un solo error
        public ApiErrorResponse FromSingle(int statusCode, string internalError, string message) =>
            new(statusCode, internalError, message);
    }
}