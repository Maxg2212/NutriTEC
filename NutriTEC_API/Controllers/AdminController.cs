using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using NutriTEC_API.Models;
using PdfSharpCore;
using PdfSharpCore.Pdf;
using System.Reflection.PortableExecutable;
using TheArtOfDev.HtmlRenderer.PdfSharp;

namespace NutriTEC_API.Controllers
{
    [ApiController]
    [Route("api/")]
    public class AdminController : ControllerBase
    {
        private readonly Proyecto2nutritecContext _context;

        public AdminController(Proyecto2nutritecContext context)
        {
            _context = context;
        }


        /// <summary>
        /// Method that validates the credentials from the administrator.
        /// </summary>
        /// <param name="Admin_Credentials">The credentials of the administrator.</param>
        /// <returns>All the information of the administrator.</returns>
        /// <remarks>This method queries a database to get the data.</remarks>
        [HttpPost("auth_admin")]
        public async Task<ActionResult<JSON_Object>> AuthAdmin(Credentials Admin_Credentials)
        {
            Admin_Credentials.password = MD5Encrypt.EncryptPassword(Admin_Credentials.password);
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

        /// <summary>
        /// Method that signs up an administrator.
        /// </summary>
        /// <param name="Admin_Credentials">All administrator information to add to the database.</param>
        /// <returns>A confimation note or an error.</returns>
        /// <remarks>This method queries a database to add the administrator.</remarks>
        [HttpPost("add_admin")]
        public async Task<ActionResult<JSON_Object>> AddAdmin(Credentials Admin_Credentials)
        {
            Admin_Credentials.password = MD5Encrypt.EncryptPassword(Admin_Credentials.password);
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertAdmins.FromSqlInterpolated($"select * from insert_admin({Admin_Credentials.email},{Admin_Credentials.password})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].insert_admin == 1)
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
        /// Method that updates the state of a product.
        /// </summary>
        /// <param name="ProductDishData">Product's identifier and state that is going to be modified.</param>
        /// <returns>A confirmation message.</returns>
        /// <remarks>This method queries a database to update a products's state .</remarks>
        [HttpPut("change_state_product_dish")]
        public async Task<ActionResult<JSON_Object>> ChangeState(ProductDishData ProductDishData)
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.ProductDishStates.FromSqlInterpolated($"select * from change_product_state({ProductDishData.barcode},{ProductDishData.state})");
            var PGSQL_result = result.ToList();

            if (PGSQL_result[0].change_product_state == 1)
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
        [HttpGet("get_aproved_product_dish")]
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
        /// Method that obtains the unapproved products.
        /// </summary>
        /// <param> None. </param>
        /// <returns>A table containing info of unapproved products.</returns>
        /// <remarks>This method queries a database to get the unapproved products.</remarks>
        [HttpGet("get_unaproved_product_dish")]
        public async Task<ActionResult<JSON_Object>> GetUnaproved()
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AllProductDishes.FromSqlInterpolated($"select * from get_unaproved_products()");
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
        /// Method that obtains the rejected products.
        /// </summary>
        /// <param> None. </param>
        /// <returns>A table containing info of rejected products.</returns>
        /// <remarks>This method queries a database to get the rejected products.</remarks>
        [HttpGet("get_rejected_product_dish")]
        public async Task<ActionResult<JSON_Object>> GetRejected()
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AllProductDishes.FromSqlInterpolated($"select * from get_rejected_products()");
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
        /// Method that obtains the all products.
        /// </summary>
        /// <param> None. </param>
        /// <returns>A table containing info of all products.</returns>
        /// <remarks>This method queries a database to get all products.</remarks>
        [HttpGet("get_all_products_dishes")]
        public async Task<ActionResult<JSON_Object>> GetAllProducts()
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.AllProductDishes.FromSqlInterpolated($"select * from get_all_products()");
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

        [HttpGet("get_payment_report/{payment_type}/{email}/{name}/{second_name}/{lname1}/{lname2}/{credit_card}/{discount}/{total_amount}/{final_amount}")]
        public async Task<ActionResult<JSON_Object>> GetPaymentReport()
        {
            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.PaymentReports.FromSqlInterpolated($"select * from payment_report()");
            var PGSQL_result = result.ToList();
            var document = new PdfDocument();
            string html = "<div style='width:100%; text-align:center'>";
            html += "<h3>   AdminReport  </h3>";
            html += "<div>";
            html += "<table style ='width:100%; border: 1px solid #000'>";
            html += "<thead style='font-weight:bold'>";
            html += "<tr>";
            html += "<td style='border:1px solid #000'>  payment_type </td>";
            html += "<td style='border:1px solid #000'>  email </td>";
            html += "<td style='border:1px solid #000'>  name </td>";
            html += "<td style='border:1px solid #000'>  second_name </td >";
            html += "<td style='border:1px solid #000'> lname1 </td>";
            html += "<td style='border:1px solid #000'> lname2 </td>";
            html += "<td style='border:1px solid #000'> credit_card </td>";
            html += "<td style='border:1px solid #000'> discount </td>";
            html += "<td style='border:1px solid #000'> total_amount </td>";
            html += "<td style='border:1px solid #000'> final_amount </td>";
            html += "</tr>";
            html += "</thead >";

            html += "<tbody>";
            
            foreach (var item in PGSQL_result)
            {
                html += "<tr>";
                html += "<td>" + item.payment_type + "</td>";
                html += "<td>" + item.email + "</td>";
                html += "<td>" + item.name + "</td >";
                html += "<td>" + item.second_name + "</td >";
                html += "<td>" + item.lname1 + "</td>";
                html += "<td> " + item.lname2 + "</td >";
                html += "<td> " + item.credit_card + "</td >";
                html += "<td> " + item.discount + "</td >";
                html += "<td> " + item.total_amount + "</td >";
                html += "<td> " + item.final_amount + "</td >";
                html += "</tr>";
            }
            html += "</tbody>";

            PdfGenerator.AddPdfPages(document, html, PageSize.A3);
            byte[]? response = null;
            using (MemoryStream stream = new MemoryStream())
            {
                document.Save(stream);
                response = stream.ToArray();
            }
            string filename = $"{"paymentreport"}.pdf";
            return File(response, "application/pdf", filename);
        }

    }
}
