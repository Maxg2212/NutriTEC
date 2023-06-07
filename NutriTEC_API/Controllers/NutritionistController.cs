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
            Nutritionist_Credentials.password = MD5Encrypt.EncryptPassword(Nutritionist_Credentials.password);
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

            Nutritionist_Data.password = MD5Encrypt.EncryptPassword(Nutritionist_Data.password);
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

        [HttpPost("create_eating_plan")]
        public async Task<ActionResult<JSON_Object>> AddEatingPlan(EatingPlanFunction eatingPlanEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertEatingPlan.FromSqlInterpolated($"select * from create_eating_plan({eatingPlanEntries.eatplan_id},{eatingPlanEntries.nutritionist_name},{eatingPlanEntries.quantity},{eatingPlanEntries.eating_schedule},{eatingPlanEntries.start_period},{eatingPlanEntries.ending_period})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].create_eating_plan == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("assign_eating_plan_to_client")]


        [HttpPost("assign_daily_consump")]
        public async Task<ActionResult<JSON_Object>> AssignDailyConsumption(DailyConsumptionFunction dailyConsumptionEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AssignDailyConsumptions.FromSqlInterpolated($"select * from assign_daily_consumption({dailyConsumptionEntries.barcode},{dailyConsumptionEntries.client_id},{dailyConsumptionEntries.eating_time},{dailyConsumptionEntries.datec})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].assign_daily_consumption == 1)
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
