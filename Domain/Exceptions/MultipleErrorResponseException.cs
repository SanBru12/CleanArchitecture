namespace Domain.Exceptions
{
    public class MultipleErrorResponseException(int statusCode, string internalError, List<string>? messages = null) : Exception(internalError)
    {
        public int StatusCode { get; set; } = statusCode;
        public string InternalError { get; set; } = internalError;
        public List<string>? Messages { get; set; } = messages ?? [];
    }
}
