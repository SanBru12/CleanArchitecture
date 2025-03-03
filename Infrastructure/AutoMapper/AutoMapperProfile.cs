using Application.Handlers.Tenants;
using AutoMapper;
using Data.Models;
using Microsoft.AspNetCore.Identity;
using Models.Dtos.Identity.Roles;
using Models.Dtos.Tenant;

namespace Api.Infrastructure.AutoMapper
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
