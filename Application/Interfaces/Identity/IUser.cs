using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Identity
{
    public interface IUser
    {
        Task CreatAsync(string UserName, string Email, string Password);
        Task<IdentityUser> GetByIdAsync(string Id);
        Task<IdentityUser> GetByNameAsync(string Name);
        Task UpdateAsync(string UserId, string UserName, string Email, string? Password);
    }
}
