using Application.Interfaces.Repositories;
using Data.Models;
using Infrastructure.Exceptions;
using MediatR;

namespace Application.Handlers.Tenants
{
    public class DeleteTenantRequest(int id) : IRequest<int>
    {
        public int Id { get; set; } = id;

        public class DeleteTenantHandler(IRepository<Tenant> context) : IRequestHandler<DeleteTenantRequest, int>
        {
            private readonly IRepository<Tenant> _context = context;

            public async Task<int> Handle(DeleteTenantRequest request, CancellationToken cancellationToken)
            {
                var dbModel = await  _context.GetByIdAsync(request.Id) ?? throw new ErrorResponseException(400, $"{typeof(DeleteTenantHandler)}", $"No se encontro el modelo con el identficador {request.Id}");
                
                await _context.Delete(dbModel);

                return request.Id;
            }
        }
    }

}