using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTEC_API.Models;

namespace NutriTEC_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AdminController: ControllerBase
    {
        private readonly Proyecto2nutritecContext _context;

        public AdminController(Proyecto2nutritecContext context)
        {
            _context = context;
        }
        [HttpPost("auth_admin")]
        public async Task<ActionResult<JSON_Object>> AuthAdmin(Credentials Admin_Credentials)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.LoginAdmins.FromSqlInterpolated($"select * from login_admin({Admin_Credentials.email},{Admin_Credentials.password})");
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
