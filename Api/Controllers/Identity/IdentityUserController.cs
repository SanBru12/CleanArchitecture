using Application.Interfaces.Identity;
using Microsoft.AspNetCore.Mvc;
using Models.Responses;
using System.Net;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace Api.Controllers.Identity
{
    [Route("api/[controller]")]
    [ApiController]
    public class IdentityUserController(IUser user) : ControllerBase
    {
        private readonly IUser _userServices = user;


        /// <summary>
        /// Crea un nuevo usuaro
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ErrorResponse"></exception>
        [HttpGet("Create")]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public async Task<ActionResult> CreateAsync()
        {

            

            await _userServices.CreatAsync();

            Ok();

        }



    }
}
