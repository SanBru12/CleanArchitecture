namespace Models.Responses
{
    public class MultipleErrorResponse(int statusCode, string internalError, List<string>? messages = null)
    {
        public int StatusCode { get; set; } = statusCode;
        public string InternalError { get; set; } = internalError;
        public List<string>? Messages { get; set; } = messages ?? [];
    }
}
