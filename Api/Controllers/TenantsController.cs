using Application.Handlers.Tenants;
using MediatR;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class TenantsController : ControllerBase
    {
        private readonly ISender _sender;

        public TenantsController(ISender sender)
        {
            _sender=sender;
        }

        [HttpPost("Create")]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateTenantAsync(CreateTenantRequest request) => Ok(await _sender.Send(request));
    }
}
