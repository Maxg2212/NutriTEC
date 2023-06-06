using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTEC_API.Models;
using System.Data;
using System.Text.Json.Nodes;
using System.Xml.Linq;

namespace NutriTEC_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class ClientController : ControllerBase
    {
        private readonly Proyecto2nutritecContext _context;
        public ClientController(Proyecto2nutritecContext context)
        {
            _context = context;
        }
        [HttpPost("auth_client")]
        public async Task<ActionResult<JSON_Object>> AuthClient(Credentials Client_Credentials)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.LoginClients.FromSqlInterpolated($"select * from login_client({Client_Credentials.email},{Client_Credentials.password})");
            var PGSQL_result = result.ToList();
            if (PGSQL_result.Count == 0)
            {
                return BadRequest(json);
            }
            else
            {
                json.status = "ok";
                json.result = PGSQL_result[0];
                return Ok(json);
            }

        }
    }
}
