using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTEC_API.Models;

namespace NutriTEC_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class NutritionistController : ControllerBase
    {       
        private readonly Proyecto2nutritecContext _context;
        public NutritionistController(Proyecto2nutritecContext context)
        {
            _context = context;
        }
        [HttpPost("auth_nutritionist")]
        public async Task<ActionResult<JSON_Object>> AuthNutritionist(Credentials Nutritionist_Credentials)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.LoginNutritionists.FromSqlInterpolated($"select * from login_nutritionist({Nutritionist_Credentials.email},{Nutritionist_Credentials.password})");
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
