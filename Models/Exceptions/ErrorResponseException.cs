namespace Infrastructure.Exceptions
{
    public class ErrorResponseException(int statusCode, string internalError, string messages) : Exception(messages)
    {
        public int StatusCode { get; set; } = statusCode;
        public string InternalError { get; set; } = internalError;
    }
}
