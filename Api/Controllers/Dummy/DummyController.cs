using Infrastructure.Configuration.Settings;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Data.SqlClient;
using Microsoft.Extensions.Options;
using Models.Responses;
using System.Net;

namespace Api.Controllers.Dummy
{
    [Route("api/[controller]")]
    [ApiController]
    public class DummyController : ControllerBase
    {
        private readonly IOptions<ConnectionStringsSettings> connectionStringsSettings;

        public DummyController(IOptions<ConnectionStringsSettings> connectionStringsSettings)
        {
            this.connectionStringsSettings=connectionStringsSettings;
        }

        /// <summary>
        /// Genera un error de prueba para verificar el manejo de excepciones.
        /// </summary>
        /// <returns></returns>
        /// <exception cref="ErrorResponse"></exception>
        [HttpGet("Error")]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public IActionResult GenerarError()
        {
            var x = 1;
            var y = 0;
            var result = x / y; // Esto generará una excepción de división por cero
            return Ok(result);
        }


        [HttpGet("ConectionSQL")]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.BadRequest)]
        [ProducesResponseType(typeof(ApiErrorResponse), (int)HttpStatusCode.InternalServerError)]
        public IActionResult ConectionSQL()
        {
            using var connection = new SqlConnection(connectionStringsSettings.Value.ConectionSQL);
            connection.Open();
            return Ok($"{connection.State}");
        }



    }
}
