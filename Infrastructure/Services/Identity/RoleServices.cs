using Application.Interfaces.Identity;
using AutoMapper;
using Infrastructure.Exceptions;
using Microsoft.AspNetCore.Identity;
using Microsoft.EntityFrameworkCore;
using Models.Dtos.Identity.Roles;

namespace Infrastructure.Services.Identity
{
    public class RoleServices(RoleManager<IdentityRole> roleManager, IMapper mapper) : IRole
    {
        private readonly RoleManager<IdentityRole> _roleManager = roleManager;
        private readonly IMapper _mapper = mapper;

        public async Task<List<GetRoleDto>> GetAsync(IdentityRole Model)
        {
            var RoleModel = await _roleManager.Roles.ToListAsync();

            List<GetRoleDto> model = _mapper.Map<List<GetRoleDto>>(RoleModel);

            return model ?? throw new ErrorResponseException(400, "", "Error al crear el rol");
        }

        public async Task<GetRoleDto> GetByIdAsync(string Id)
        {
            var RoleModel = await _roleManager.Roles.FirstOrDefaultAsync(x => x.Id == Id);

            GetRoleDto model = _mapper.Map<GetRoleDto>(RoleModel);

            return model ?? throw new ErrorResponseException(400, "", "Error al crear el rol");
        }

        public async Task CreateAsync(IdentityRole Model)
        {
            var identityResult = await _roleManager.CreateAsync(Model);

            if (!identityResult.Succeeded) throw new ErrorResponseException(400, "", "Error al crear el rol");
        }

        public async Task UpdateAsync(IdentityRole Model)
        {
            var identityResult = await _roleManager.UpdateAsync(Model);

            if (!identityResult.Succeeded) throw new ErrorResponseException(400, "", "Error al crear el rol");
        }
    }
}
