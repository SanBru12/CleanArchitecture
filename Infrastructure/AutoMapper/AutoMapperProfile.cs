using Application.Common.Tenant;
using AutoMapper;
using Domain.Entities;
using Microsoft.AspNetCore.Identity;
using Models.Dtos.Identity.Roles;

namespace Infrastructure.AutoMapper
{
    public class AutoMapperProfile : Profile
    {
        public AutoMapperProfile()
        {
            CreateMap<Tenant, GetTenantDto>();

            CreateMap<IdentityRole, GetRoleDto>();
        }
    }
}
