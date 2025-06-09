namespace Domain.Entities
{
    public class AuditLog
    {
        public int Id { get; set; }
        public string? TableId { get; set; } = "";
        public string? TableName { get; set; } = "";
        public string? KeyValues { get; set; } = "";
        public string? OldValues { get; set; } = "";
        public string? NewValues { get; set; } = "";
        public string? Action { get; set; } = "";
        public string? UserId { get; set; } = "";
        public string? RemoteIpAddress { get; set; } = "";
    }
}
