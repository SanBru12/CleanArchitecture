using Microsoft.AspNetCore.Identity;

namespace Application.Interfaces.Identity
{
    public interface IAuth
    {
        Task<SignInResult> LoginAsync(string Username, string Password);
    }
}
