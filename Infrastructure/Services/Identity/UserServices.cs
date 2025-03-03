using Application.Interfaces.Identity;
using Infrastructure.DataAccess;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;

namespace Infrastructure.Services.Identity
{
    public class UserServices(UserManager<IdentityUser> userManager, AppDbContext appDbContext) : IUser
    {
        private readonly UserManager<IdentityUser> _userManager = userManager;
        private readonly AppDbContext _context = appDbContext;

        public async Task<IdentityUser> GetByIdAsync(string Id)
        {
            var user = await _userManager.FindByIdAsync(Id);

            return user ?? throw new ErrorResponseException(404, "", "Usuario no encontrado");
        }

        public async Task<IdentityUser> GetByNameAsync(string Name)
        {
            var user = await _userManager.FindByNameAsync(Name);

            return user ?? throw new ErrorResponseException(404, "", "Usuario no encontrado");
        }

        public async Task CreatAsync(string UserName, string Email, string Password)
        {
            IdentityUser Model = new()
            {
                UserName = UserName,
                Email = Email,
            };

            var identityResult = await _userManager.CreateAsync(Model, Password);

            if (!identityResult.Succeeded) throw new ErrorResponseException(400, "", "Error al crear el usuario");
        }

        public async Task UpdateAsync(string UserId, string UserName, string Email, string? Password)
        {
            var Model = await GetByIdAsync(UserId);

            Model.UserName = UserName;

            Model.Email = Email;

            var identityResult = await _userManager.UpdateAsync(Model);

            if (!identityResult.Succeeded) throw new ErrorResponseException(400, "", "Error al modificar el usuario");

            if(!string.IsNullOrEmpty(Password))
            {
                var Token = await _userManager.GeneratePasswordResetTokenAsync(Model);

                var resultPassword = await _userManager.ResetPasswordAsync(Model, Token, Password);

                if (!resultPassword.Succeeded) throw new ErrorResponseException(400, "", "Error al modificar la contraseña");
            }

        }
    }
}
