namespace Models.Dtos.Identity.Roles
{
    public class GetRoleDto
    {
        public string Id { get; set; } = default!;
        public string Name { get; set; } = default!;
        public string? Description { get; set; }
        public List<string>? Permissions { get; set; }
    }
}
