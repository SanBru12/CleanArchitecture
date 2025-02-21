namespace Models.Responses
{
    public class ErrorResponse
    {
        public int StatusCode { get; set; }
        public string Message { get; set; }
        public List<string>? MessageDetail { get; set; }
        public string InternalError { get; set; }

        public ErrorResponse(int statusCode, string internalError, string message, List<string>? messageDetail = null)
        {
            StatusCode = statusCode;
            Message = message;
            InternalError = internalError;
            MessageDetail = messageDetail ?? new List<string>();
        }
    }
}
