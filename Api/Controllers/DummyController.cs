using Microsoft.AspNetCore.Mvc;
using Models.Exceptions;
using Models.Responses;
using System.Net;

namespace Api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        /// <summary>
        /// Genera un error de prueba para verificar el manejo de excepciones.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ErrorResponseException"></exception>
        [HttpGet("Error")]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GenerarError()
        {
            throw new ErrorResponseException(400, "El parámetro X es inválido.", ["Prueba de error"]);
        }
    }
}
