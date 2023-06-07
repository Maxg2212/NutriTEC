using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTEC_API.Models;
using System.Globalization;

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

        [HttpPost("add_nutritionist")]
        public async Task<ActionResult<JSON_Object>> AddNutritionist(NutritionistData Nutritionist_Data)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertNutritionist.FromSqlInterpolated($"select * from insert_nutritionist({Nutritionist_Data.employee_id},{Nutritionist_Data.email},{Nutritionist_Data.name},{Nutritionist_Data.second_name},{Nutritionist_Data.lname1},{Nutritionist_Data.lname2},{Nutritionist_Data.password},{DateOnly.ParseExact(Nutritionist_Data.bdate, "yyyy-MM-dd", CultureInfo.InvariantCulture)},{Nutritionist_Data.profile_pic},{Nutritionist_Data.credit_card},{Nutritionist_Data.nutritionist_code},{Nutritionist_Data.bmi},{Nutritionist_Data.weight},{Nutritionist_Data.address},{Nutritionist_Data.payment_type})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].insert_nutritionist == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }
    }
}
