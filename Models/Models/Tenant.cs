using Models.Entity;

namespace Data.Models
{
    public class Tenant : AuditableEntity
    {
        public int Id { get; set; }
        public string? Name { get; set; } = "";
        public string? Cuit { get; set; } = "";
        public bool? Active { get; set; } = true;

        public Tenant Update(string? name, string? cuit, bool? active)
        {
            if (Name is not null && Name?.Equals(name) is not true) Name = name;
            if (Cuit is not null && Cuit?.Equals(cuit) is not true) Cuit = cuit;
            if (Active is not null && Active?.Equals(active) is not true) Active = active;
            return this;
        }

    }
}
