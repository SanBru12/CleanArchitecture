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


      



    }
}
