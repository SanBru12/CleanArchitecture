using Microsoft.AspNetCore.Identity;
using Models.Dtos.Identity.Roles;

namespace Application.Interfaces.Identity
{
    public interface IRole
    {
        Task CreateAsync(IdentityRole Model);
        Task<List<GetRoleDto>> GetAsync(IdentityRole Model);
        Task<GetRoleDto> GetByIdAsync(string Id);
        Task UpdateAsync(IdentityRole Model);
    }
}
