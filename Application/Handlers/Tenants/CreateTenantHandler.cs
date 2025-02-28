using MediatR;

namespace Application.Handlers.Tenants
{
    public class CreateTenantRequest : IRequest<string>
    {
        public string Name { get; set; } = "";
    }

    public class CreateTenantHandler : IRequestHandler<CreateTenantRequest, string>
    {
        public async Task<string> Handle(CreateTenantRequest request, CancellationToken cancellationToken)
        {
            await Task.Delay(1, cancellationToken);

            return request.Name;
        }
    }
}
