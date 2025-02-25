namespace Models.Responses
{
    public class ErrorResponse(int statusCode, string internalError, string message)
    {
        public int StatusCode { get; set; } = statusCode;
        public string InternalError { get; set; } = internalError;
        public string Message { get; set; } = message;
    }
}

