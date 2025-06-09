using Application.Interfaces.Repositories;
using Domain.Entities;
using Domain.Exceptions;
using MediatR;

namespace Application.Handlers.Tenants
{
    public class UpdateTenantRequest : IRequest<int>
    {
        public int Id { get; set; } 
        public string? Name { get; set; }
        public string? Cuit { get; set; }
        public bool? Active { get; set; } 
    }

    public class UpdateTenantHandler : IRequestHandler<UpdateTenantRequest, int>
    {
        private readonly IRepository<Tenant> _context;
        public UpdateTenantHandler(IRepository<Tenant> context)
        {
            _context = context;
        }
     

        public async Task<int> Handle(UpdateTenantRequest request, CancellationToken cancellationToken)
        {
            //TODO: Replace for automapper
            Tenant dbModel = await _context.GetByIdAsync(request.Id) ?? throw new ErrorResponseException(400, $"{typeof(UpdateTenantHandler)}", $"No se encontro el modelo con el identficador {request.Id}");

            Tenant result = dbModel.Update(request.Name, request.Cuit, request.Active);

            await _context.Update(result);

            return result.Id;
        }
    }
}
