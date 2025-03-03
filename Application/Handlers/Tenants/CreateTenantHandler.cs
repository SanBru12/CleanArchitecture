using Application.Interfaces.Repositories;
using Data.Models;
using MediatR;

namespace Application.Handlers.Tenants
{
    public class CreateTenantRequest : IRequest<int>
    {
        public string Name { get; set; } = "";
        public string Cuit { get; set; } = "";
        public bool Active { get; set; } = true;
    }

    public class CreateTenantHandler(IRepository<Tenant> context) : IRequestHandler<CreateTenantRequest, int>
    {
        private readonly IRepository<Tenant> _context = context;

        public async Task<int> Handle(CreateTenantRequest request, CancellationToken cancellationToken)
        {
            //TODO: Replace for automapper
            Tenant dbModel = new()
            {
                Name = request.Name,
                Cuit = request.Cuit,
                Active = request.Active
            };

            var result = await _context.Add(dbModel);

            return result.Id;
        }
    }
}
