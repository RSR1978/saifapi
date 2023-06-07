using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;
// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GoverController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public GoverController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]

        public JsonResult Get()
        {
            //string query = @"select CategoryId,SCatDesc,SCatAdesc,SCatParent,STerminal from bluecode_soqak.CATEGORIES";
            string query = @"select GoverId,GoverDesc,GoverAdesc from sooqak.Govers";
            DataTable table = new DataTable();


            string mysqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(mysqlDataSource))
            {
                myCon.Open();
                MySqlCommand myCommand = new MySqlCommand(query, myCon);
                myReader = myCommand.ExecuteReader();
                table.Load(myReader);
                myReader.Close();
                myCon.Close();
            }

            return new JsonResult(table);

        }
    }
}
