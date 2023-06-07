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

        [HttpPost("add_daily_consumption")]
        public async Task<ActionResult<JSON_Object>> AddDayliConsumption(ClientMeasures Client_Info)
        {

            JSON_Object json = new JSON_Object("error", null); //Se inicializa con error y null para ver si hay algun error.
            var result = _context.InsertClient.FromSqlInterpolated($"select * from assign_daily_consumption({Client_Info.client_id},{Client_Info.muslce_percentage}, {Client_Info.fat_percentage},{Client_Info.hip_size},{Client_Info.waist_size},{Client_Info.neck_size},{Client_Info.last_month_meas})");
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
    }
}
