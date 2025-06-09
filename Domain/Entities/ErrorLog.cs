namespace Domain.Entities
{
    public class ErrorLog
    {
        public int Id { get; set; }
        public string? Type { get; set; } = "";
        public string? Message { get; set; } = "";
        public string? StackTrace { get; set; } = "";
        public string? Source { get; set; } = "";
        public string? TargetSite { get; set; } = "";
        public string? InnerException { get; set; } = "";
        public DateTime? CreateDate { get; set; }
        public string? Path { get; set; } = "";
        public string? Method { get; set; } = "";
    }
}
