using Application.Interfaces.Repositories;
using Domain.Entities;
using MediatR;
using Shared.Exceptions;

namespace Application.Handlers.Tenants
{
    public class DeleteTenantRequest(long id) : IRequest<long>
    {
        public long Id { get; set; } = id;

        public class DeleteTenantHandler(IRepository<Tenant> context) : IRequestHandler<DeleteTenantRequest, long>
        {
            private readonly IRepository<Tenant> _context = context;

            public async Task<long> Handle(DeleteTenantRequest request, CancellationToken cancellationToken)
            {
                var dbModel = await  _context.GetByIdAsync(request.Id) ?? throw new ErrorResponseException(400, $"{typeof(DeleteTenantHandler)}", $"No se encontro el modelo con el identficador {request.Id}");
                
                await _context.Delete(dbModel);

                return request.Id;
            }
        }
    }

}