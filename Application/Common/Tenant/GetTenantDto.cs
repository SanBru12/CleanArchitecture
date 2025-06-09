namespace Application.Common.Tenant
{
    public class GetTenantDto
    {
        public int Id { get; set; }
        public string? Name { get; set; } 
        public string? Cuit { get; set; } 
        public bool? Active { get; set; } 
    }
}
