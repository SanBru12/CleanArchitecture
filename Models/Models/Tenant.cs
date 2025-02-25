using Models.Entity;

namespace Data.Models
{
    public class Tenant : AuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "";
        public string? Active { get; set; } = "";
    }
}
