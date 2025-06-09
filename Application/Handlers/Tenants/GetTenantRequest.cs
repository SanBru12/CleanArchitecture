using Application.Common.Tenant;
using Application.Interfaces.Repositories;
using AutoMapper;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Handlers.Tenants
{
    public class GetTenantRequest : IRequest<GetTenantDto>
    {
        public int Id { get; set; } 

        public class GetTenantHandler(IRepository<Tenant> context, IMapper mapper) : IRequestHandler<GetTenantRequest, GetTenantDto>
        {
            private readonly IRepository<Tenant> _context = context;
            private readonly IMapper _mapper = mapper;

            public async Task<GetTenantDto> Handle(GetTenantRequest request, CancellationToken cancellationToken)
            {
                var dbModel = await _context.GetByIdAsync(request.Id) ?? throw new ErrorResponseException(400, $"{typeof(GetTenantRequest)}", $"No se encontro el modelo con el identficador {request.Id}");

                GetTenantDto model = _mapper.Map<GetTenantDto>(dbModel);

                return model;
            }
        }
    }
}
