using Application.Interfaces.Identity;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Identity
{
    public class UserServices(UserManager<IdentityUser> userManager) : IUser
    {
        private readonly UserManager<IdentityUser> _userContext = userManager;



    }
}
