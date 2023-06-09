using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTEC_API.Models;
using System.Data;
using System.Globalization;
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

        /// <summary>
        /// Method that validates the credentials from the client.
        /// </summary>
        /// <param name="Client_Credentials">The credentials of the client.</param>
        /// <returns>All the information of the administrator.</returns>
        /// <remarks>This method queries a database to get the data.</remarks>
        [HttpPost("auth_client")]
        public async Task<ActionResult<JSON_Object>> AuthClient(Credentials Client_Credentials)
        {
            Client_Credentials.password = MD5Encrypt.EncryptPassword(Client_Credentials.password);
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

        /// <summary>
        /// Method that signs up a client.
        /// </summary>
        /// <param name="Client_Info">All client's information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to add the client.</remarks>
        [HttpPost("add_client")]
        public async Task<ActionResult<JSON_Object>> AddClient(ClientData Client_Info)
        {
            Client_Info.password = MD5Encrypt.EncryptPassword(Client_Info.password);
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertClient.FromSqlInterpolated($"select * from insert_client({Client_Info.client_id},{Client_Info.name}, {Client_Info.second_name},{Client_Info.lname1},{Client_Info.lname2},{Client_Info.weight},{Client_Info.bmi},{Client_Info.password},{Client_Info.email},{DateOnly.ParseExact(Client_Info.bdate, "yyyy-MM-dd", CultureInfo.InvariantCulture)},{Client_Info.muslce_percentage},{Client_Info.fat_percentage},{Client_Info.hip_size},{Client_Info.waist_size},{Client_Info.neck_size},{Client_Info.last_month_meas})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].insert_client == 1)
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
        /// Method that updates a client's measurements, this can be performed by nutritionist and client.
        /// </summary>
        /// <param name="Client_Info">All client's data that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a client's measurements.</remarks>
        [HttpPut("update_measures")]
        public async Task<ActionResult<JSON_Object>> UpdateMeasures(ClientMeasures Client_Info)
        {

            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertClient.FromSqlInterpolated($"select * from update_client_measurements({Client_Info.client_id},{Client_Info.muslce_percentage}, {Client_Info.fat_percentage},{Client_Info.hip_size},{Client_Info.waist_size},{Client_Info.neck_size},{Client_Info.last_month_meas})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].insert_client == 1)
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
        /// Method that assigns a daily consumption to the database, this method can be performed by nutritionist and client.
        /// </summary>
        /// <param name="dailyConsumptionEntries">All daily consumption information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to assign the daily consumption.</remarks>
        [HttpPost("assign_daily_consumption")]
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
        /// Method that searches a recipe, this method can be performed by the nutritionist and the client.
        /// </summary>
        /// <param name="recipe_Id">A recipe identifier to get its info from the database.</param>
        /// <returns>A table containing a recipe's info.</returns>
        /// <remarks>This method queries a database to search a recipe.</remarks>
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

        /// <summary>
        /// Method that inserts a recipe.
        /// </summary>
        /// <param name="recipeData">All recipe data to to insert into the database.</param>
        /// <returns>A confirmation message..</returns>
        /// <remarks>This method queries a database to insert the recipe.</remarks>
        [HttpPost("insert_recipe")]
        public async Task<ActionResult<JSON_Object>> InsertRecipe(RecipeData recipeData)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertRecipe.FromSqlInterpolated($"select * from insert_recipe({recipeData.recipe_id},{recipeData.portions},{recipeData.calories},{recipeData.ingredients})");
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
        /// Method that updates a recipe, this method can be performed by the nutritionist and the client.
        /// </summary>
        /// <param name="recipeData">All recipe data that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a recipe.</remarks>
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


        /// <summary>
        /// Method that deletes a recipe, this method can be performed by nutritionist and client.
        /// </summary>
        /// <param name="recipe_Id">Recipe identifier.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to delete recipe.</remarks>
        [HttpPut("delete_recipe")]
        public async Task<ActionResult<JSON_Object>> DeleteRecipe(RecipeId recipe_Id)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.DeleteRecipe.FromSqlInterpolated($"select * from delete_recipe({recipe_Id.recipe_id})");
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
        /// Method that obtains the approved products.
        /// </summary>
        /// <param> None. </param>
        /// <returns>A table containing info of approved products.</returns>
        /// <remarks>This method queries a database to get the approved products.</remarks>
        [HttpGet("client_get_aproved_product_dish")]
        public async Task<ActionResult<JSON_Object>> GetAproved()
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AllProductDishes.FromSqlInterpolated($"select * from get_aproved_products()");
            var PGSQL_result = result.ToList();

            if (PGSQL_result.Count == 0)
            {
                return BadRequest(json);
            }
            else
            {
                json.status = "ok";
                json.result = PGSQL_result;
                return Ok(json);

            }
        }

        /// <summary>
        /// Method that obtains the eating plan of a client.
        /// </summary>
        /// <param name="client_id">Client identifier to obtain their eating plan.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to get the client's eating plan.</remarks>
        [HttpPost("client_get_eating_plan")]
        public async Task<ActionResult<JSON_Object>> GetClientEatingPlan(ClientId client_id)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.ClientEatingPlan.FromSqlInterpolated($"select * from get_client_eating_plan({client_id.client_id})");
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
        /// Method that obtains the nutritionist of a client.
        /// </summary>
        /// <param name="client_id">Client identifier to obtain their nutritionist.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to get the client's nutritionist.</remarks>
        [HttpPost("get_nutritionist_by_client")]
        public async Task<ActionResult<JSON_Object>> GetNutritionistByClient(ClientId client_id)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.GetNutritionistByClientData.FromSqlInterpolated($"select * from get_nutritionist_by_client({client_id.client_id})");
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
        /// Method that updates a product.
        /// </summary>
        /// <param name="productUpdated">All product's data that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a products's information .</remarks>
        [HttpPut("client_update_product")]
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

        /// <summary>
        /// Method that inserts a product.
        /// </summary>
        /// <param name="productDishInserts">All product data to to insert into the database.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to insert the product.</remarks>
        [HttpPost("client_insert_product_dish")]
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



    }
}
