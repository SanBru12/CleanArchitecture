using Api.Exceptions;
using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Exceptions;
using Shared.Responses;

namespace Application.Handlers.Tenants
{
    public class UpdateTenantRequest : IRequest<long>
    {
        public long Id { get; set; } 
        public string? Name { get; set; }
        public string? Cuit { get; set; }
        public bool? Active { get; set; } 
    }

    public class UpdateTenantHandler : IRequestHandler<UpdateTenantRequest, long>
    {
        private readonly IRepository<Tenant> _context;
        public UpdateTenantHandler(IRepository<Tenant> context)
        {
            _context = context;
        }
     
        public async Task<long> Handle(UpdateTenantRequest request, CancellationToken cancellationToken)
        {
            //TODO: Replace for automapper
            Tenant dbModel = await _context.GetByIdAsync(request.Id) ?? throw new ErrorResponseException(400, $"{typeof(UpdateTenantHandler)}", $"No se encontro el modelo con el identficador {request.Id}");

            Tenant result = dbModel.Update(request.Name, request.Cuit, request.Active);

            await _context.Update(result);

            return result.Id;
        }
    }
}
