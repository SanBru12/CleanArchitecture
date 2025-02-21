namespace Models.Exceptions
{
    public class ErrorResponseException : Exception
    {
        public int StatusCode { get; set; }
        public List<string>? Messages { get; set; }
        public string InternalError { get; set; }

        public ErrorResponseException(int statusCode, string internalError, List<string>? messages = null) : base(internalError)
        {
            StatusCode = statusCode;
            InternalError = internalError;
            Messages = messages ?? [];
        }
    }
}
