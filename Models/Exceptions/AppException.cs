namespace Shared.Exceptions
{
    public abstract class AppException : Exception
    {
        public int StatusCode { get; init; }
        public string InternalError { get; init; }
        public List<string>? Messages { get; init; }

        protected AppException(int statusCode, string internalError, string? message = null, List<string>? messages = null)
            : base(message ?? internalError)
        {
            StatusCode = statusCode;
            InternalError = internalError;
            Messages = messages?.Count > 0 ? messages : null;
        }
    }

    public class ErrorResponseException : AppException
    {
        public ErrorResponseException(int statusCode, string internalError, string message)
            : base(statusCode, internalError, message) { }
    }

    public class MultipleErrorResponseException : AppException
    {
        public MultipleErrorResponseException(int statusCode, string internalError, List<string>? messages = null)
            : base(statusCode, internalError, null, messages) { }
    }
}
