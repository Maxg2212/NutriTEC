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
        //*****************************Login*****************************88
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

        //*******************Registro**************
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

        //****************************Gestion planes************************8
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
        public async Task<ActionResult<JSON_Object>> AssignEatingPlanToClient(EatingPlanToClient eatingPlanToClientEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AssignEatingPlanClients.FromSqlInterpolated($"select * from assign_eating_plan_to_client({eatingPlanToClientEntries.client_id},{eatingPlanToClientEntries.nutritionist_id},{eatingPlanToClientEntries.eatplan_id})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].assign_eating_plan_to_client == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        //******************************Asignación de planes**************
        [HttpPost("assign_daily_consump")]
        public async Task<ActionResult<JSON_Object>> AssignDailyConsumption(DailyConsumptionFunction dailyConsumptionEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AssignDailyConsumptions.FromSqlInterpolated($"select * from assign_daily_consumption({dailyConsumptionEntries.barcode},{dailyConsumptionEntries.client_id},{dailyConsumptionEntries.eating_time},{DateOnly.ParseExact(dailyConsumptionEntries.datec, "yyyy-MM-dd", CultureInfo.InvariantCulture)})");
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


        [HttpPost("assign_product_to_recipe")]
        public async Task<ActionResult<JSON_Object>> AssignProductToRecipe(ProductToRecipeFunction productRecipeFunctionEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AssignProductToRecipes.FromSqlInterpolated($"select * from assign_product_to_recipe({productRecipeFunctionEntries.barcode},{productRecipeFunctionEntries.recipe_id}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].assign_product_to_recipe == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPut("delete_recipe_from_nutri")]
        public async Task<ActionResult<JSON_Object>> DeleteRecipeFromNutri(RecipeIdentifier productRecipeFunctionEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.DeleteRecipeFunctions.FromSqlInterpolated($"select * from delete_recipe({productRecipeFunctionEntries.recipe_id}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].delete_recipe == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("get_client_eating_plan")]
        public async Task<ActionResult<JSON_Object>> GetClientEatingPlan(ClientIdentifier clientIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetClientEatingPlans.FromSqlInterpolated($"select * from get_client_eating_plan({clientIdentifier.client_id}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].get_client_eating_plan == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("get_eating_plan")]
        public async Task<ActionResult<JSON_Object>> GetEatingPlan(EatingPlanIdentifier eatingPlanIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetEatingPlans.FromSqlInterpolated($"select * from get_eating_plan({eatingPlanIdentifier.eatplan_id}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].get_eating_plan == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("get_products_by_recipe")]
        public async Task<ActionResult<JSON_Object>> GetProductsByRecipe(RecipeIdentifier recipeIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetProductsByRecipes.FromSqlInterpolated($"select * from get_products_by_recipe({recipeIdentifier.recipe_id}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].get_products_by_recipe == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("insert_product_dish")]
        public async Task<ActionResult<JSON_Object>> InsertProductDish(ProductDishInserts productDishInserts)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.ProductDishFunctions.FromSqlInterpolated($"select * from insert_product_dish({productDishInserts.barcode},{productDishInserts.vitamins},{productDishInserts.calcium},{productDishInserts.iron},{productDishInserts.description},{productDishInserts.portion_size},{productDishInserts.energy},{productDishInserts.fat},{productDishInserts.sodium},{productDishInserts.carbs},{productDishInserts.protein},{productDishInserts.state}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].insert_product_dish == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("insert_recipe")]
        public async Task<ActionResult<JSON_Object>> InsertProductRecipe(RecipeInserts recipeInserts)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertRecipeFunctions.FromSqlInterpolated($"select * from insert_recipe({recipeInserts.recipe_id},{recipeInserts.portions},{recipeInserts.calories},{recipeInserts.ingredients}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].insert_recipe == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("search_client")]
        public async Task<ActionResult<JSON_Object>> SearchClient(ClientIdentifier clientInserts)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.SearchClients.FromSqlInterpolated($"select * from search_client({clientInserts.client_id}))");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].search_client == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }
        }

        [HttpPost("search_recipe")]
        public async Task<ActionResult<JSON_Object>> GetRecipe(RecipeId recipe_Id)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.SearchRecipe.FromSqlInterpolated($"select * from search_recipe({recipe_Id.recipe_id})");
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

        [HttpPut("update_recipe")]
        public async Task<ActionResult<JSON_Object>> UpdateRecipe(RecipeData recipeData)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.UpdateRecipe.FromSqlInterpolated($"select * from update_recipe({recipeData.recipe_id},{recipeData.portions},{recipeData.calories},{recipeData.ingredients})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].update_recipe == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }

        }

        [HttpPost("search_product")]
        public async Task<ActionResult<JSON_Object>> SearchProduct(ProductIdentifier product_Id)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.ProductIdentifiers.FromSqlInterpolated($"select * from search_recipe({product_Id.barcode})");
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

        [HttpPut("update_client_measurements")]
        public async Task<ActionResult<JSON_Object>> UpdateClientMeasurements(ClientMeasurements clientMeasurements)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.UpdateClientMeasurements.FromSqlInterpolated($"select * from update_client_measurements({clientMeasurements.client_id},{clientMeasurements.muslce_percentage},{clientMeasurements.fat_percentage},{clientMeasurements.hip_size},{clientMeasurements.waist_size},{clientMeasurements.neck_size},{clientMeasurements.last_month_meas})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].update_client_measurements == 1)
            {
                json.status = "ok";
                return Ok(json);

            }
            else
            {
                return BadRequest(json);
            }

        }

        [HttpPut("update_product")]
        public async Task<ActionResult<JSON_Object>> UpdateProduct(ProductUpdated productUpdated)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.UpdateProductFunctions.FromSqlInterpolated($"select * from update_product({productUpdated.barcode},{productUpdated.vitamins},{productUpdated.calcium},{productUpdated.iron},{productUpdated.description},{productUpdated.portion_size},{productUpdated.energy}, {productUpdated.fat}, {productUpdated.sodium},{productUpdated.carbs},{productUpdated.protein})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].update_product == 1)
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
