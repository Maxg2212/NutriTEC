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

        /// <summary>
        /// Method that validates the credentials from the nutritionist.
        /// </summary>
        /// <param name="Nutritionist_Credentials">The credentials of the nutritionist.</param>
        /// <returns>All the information of the nutritionist.</returns>
        /// <remarks>This method queries a database to get the data.</remarks>
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
        /// <summary>
        /// Method that signs up a nutritionist.
        /// </summary>
        /// <param name="Nutritionist_Data">All nutritionist information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to add the nutritionist.</remarks>
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

        /// <summary>
        /// Method that adds a eating plan.
        /// </summary>
        /// <param name="eatingPlanEntries">All eating plan information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to add eating plan.</remarks>
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
        /// <summary>
        /// Method that assigns an eating plan to a client.
        /// </summary>
        /// <param name="eatingPlanToClientEntries">All information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to assign the eating plan to a client.</remarks>
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
        /// <summary>
        /// Method that assigns a daily consumption to the database, this method can be performed by nutritionist and client.
        /// </summary>
        /// <param name="dailyConsumptionEntries">All daily consumption information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to assign the daily consumption.</remarks>
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

        /// <summary>
        /// Method that assigns a product to recipe.
        /// </summary>
        /// <param name="productRecipeFunctionEntries">Product and recipe identifiers.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to assign the product to recipe.</remarks>
        [HttpPost("assign_product_to_recipe")]
        public async Task<ActionResult<JSON_Object>> AssignProductToRecipe(ProductToRecipeFunction productRecipeFunctionEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AssignProductToRecipes.FromSqlInterpolated($"select * from assign_product_to_recipe({productRecipeFunctionEntries.barcode},{productRecipeFunctionEntries.recipe_id})");
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

        /// <summary>
        /// Method that deletes a recipe, this method can be performed by nutritionist and client.
        /// </summary>
        /// <param name="productRecipeFunctionEntries">Recipe identifier.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to delete recipe.</remarks>
        [HttpPut("delete_recipe_from_nutri")]
        public async Task<ActionResult<JSON_Object>> DeleteRecipeFromNutri(RecipeIdentifier productRecipeFunctionEntries)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.DeleteRecipeFunctions.FromSqlInterpolated($"select * from delete_recipe({productRecipeFunctionEntries.recipe_id})");
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

        /// <summary>
        /// Method that obtains the eating plan of a client.
        /// </summary>
        /// <param name="clientIdentifier">Client identifier to obtain their eating plan.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to get the client's eating plan.</remarks>
        [HttpPost("get_client_eating_plan")]
        public async Task<ActionResult<JSON_Object>> GetClientEatingPlan(ClientId clientIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetClientEatingPlans.FromSqlInterpolated($"select * from get_client_eating_plan({clientIdentifier.client_id})");
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

        /// <summary>
        /// Method that obtains the eating plan.
        /// </summary>
        /// <param name="eatingPlanIdentifier">Eating Plan identifier to obtain all info from it.</param>
        /// <returns>A table containing all the eating plan info.</returns>
        /// <remarks>This method queries a database to get the eating plan.</remarks>
        [HttpPost("get_eating_plan")]
        public async Task<ActionResult<JSON_Object>> GetEatingPlan(EatingPlanIdentifier eatingPlanIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetEatingPlans.FromSqlInterpolated($"select * from get_eating_plan({eatingPlanIdentifier.eatplan_id})");
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


        /// <summary>
        /// Method that obtains the products by recipe.
        /// </summary>
        /// <param name="recipeIdentifier">Recipe identifier to obtain all info from it.</param>
        /// <returns>A table containing all the products related to that recipe.</returns>
        /// <remarks>This method queries a database to get the products by recipe.</remarks>
        [HttpPost("get_products_by_recipe")]
        public async Task<ActionResult<JSON_Object>> GetProductsByRecipe(RecipeIdentifier recipeIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetProductsByRecipes.FromSqlInterpolated($"select * from get_products_by_recipe({recipeIdentifier.recipe_id})");
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

        /// <summary>
        /// Method that inserts a product.
        /// </summary>
        /// <param name="productDishInserts">All product data to to insert into the database.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to insert the product.</remarks>
        [HttpPost("insert_product_dish")]
        public async Task<ActionResult<JSON_Object>> InsertProductDish(ProductDishInserts productDishInserts)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.ProductDishFunctions.FromSqlInterpolated($"select * from insert_product_dish({productDishInserts.barcode},{productDishInserts.vitamins},{productDishInserts.calcium},{productDishInserts.iron},{productDishInserts.description},{productDishInserts.portion_size},{productDishInserts.energy},{productDishInserts.fat},{productDishInserts.sodium},{productDishInserts.carbs},{productDishInserts.protein})");
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

        /// <summary>
        /// Method that inserts a recipe.
        /// </summary>
        /// <param name="recipeInserts">All recipe data to to insert into the database.</param>
        /// <returns>A confirmation message..</returns>
        /// <remarks>This method queries a database to insert the recipe.</remarks>
        [HttpPost("nutri_insert_recipe")]
        public async Task<ActionResult<JSON_Object>> NutriInsertProductRecipe(RecipeInserts recipeInserts)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertRecipeFunctions.FromSqlInterpolated($"select * from insert_recipe({recipeInserts.recipe_id},{recipeInserts.portions},{recipeInserts.calories},{recipeInserts.ingredients})");
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

        /// <summary>
        /// Method that searches a client.
        /// </summary>
        /// <param name="clientInserts">A client identifier to get their info from the database.</param>
        /// <returns>A table containing a client's info.</returns>
        /// <remarks>This method queries a database to search a client.</remarks>
        [HttpPost("nutri_search_client")]
        public async Task<ActionResult<JSON_Object>> SearchClient(ClientId clientInserts)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.SearchClients.FromSqlInterpolated($"select * from search_client({clientInserts.client_id})");
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

        /// <summary>
        /// Method that searches a recipe, this method can be performed by the nutritionist and the client.
        /// </summary>
        /// <param name="recipe_Id">A recipe identifier to get its info from the database.</param>
        /// <returns>A table containing a recipe's info.</returns>
        /// <remarks>This method queries a database to search a recipe.</remarks>
        [HttpPost("nutri_search_recipe")]
        public async Task<ActionResult<JSON_Object>> NutriGetRecipe(RecipeId recipe_Id)
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

        /// <summary>
        /// Method that updates a recipe, this method can be performed by the nutritionist and the client.
        /// </summary>
        /// <param name="recipeData">All recipe data that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a recipe.</remarks>
        [HttpPut("nutri_update_recipe")]
        public async Task<ActionResult<JSON_Object>> NutriUpdateRecipe(RecipeData recipeData)
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

        /// <summary>
        /// Method that searches a product.
        /// </summary>
        /// <param name="product_Id">Product's identifieer to obtain all its info.</param>
        /// <returns>A table containing all product's info.</returns>
        /// <remarks>This method queries a database to search a product.</remarks>
        [HttpPost("nutri_search_product")]
        public async Task<ActionResult<JSON_Object>> NutriSearchProduct(ProductIdentifier product_Id)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.NutriSearchProducts.FromSqlInterpolated($"select * from search_product({product_Id.barcode})");
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

        /// <summary>
        /// Method that updates a client's measurements, this can be performed by nutritionist and client.
        /// </summary>
        /// <param name="clientMeasurements">All client's data that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a client's measurements.</remarks>
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

        /// <summary>
        /// Method that updates a product.
        /// </summary>
        /// <param name="productUpdated">All product's data that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a products's information .</remarks>
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

        //*********************sigue en pruebas***************************
        [HttpPost("get_nutritionist_plan")]
        public async Task<ActionResult<JSON_Object>> GetNutritionistPlan(NutritionistIdentifier nutriID)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetNutritionistPlans.FromSqlInterpolated($"select * from get_nutritionist_plan({nutriID.nutritionist_id})");
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

        [HttpPost("get_client_by_nutritionist")]
        public async Task<ActionResult<JSON_Object>> GetClientByNutritionist(NutritionistIdentifier nutriID)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetClientByNutritionists.FromSqlInterpolated($"select * from get_client_by_nutritionist({nutriID.nutritionist_id})");
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

        [HttpPost("get_eatplan_by_nutritionist")]
        public async Task<ActionResult<JSON_Object>> GetEatplanByNutritionist(EmployeeIdentifier employeeIdentifier)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetEatplanByNutritionists.FromSqlInterpolated($"select * from get_eatplan_by_nutritionist({employeeIdentifier.employee_id})");
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
