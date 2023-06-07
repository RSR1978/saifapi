using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;



using Microsoft.Extensions.Configuration;
using System.Data;
using MySql.Data.MySqlClient;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class CATEGORIESController : ControllerBase
    {
        private readonly IConfiguration _configuration;

        public CATEGORIESController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]

        public JsonResult Get()
        {
            //string query = @"select CategoryId,SCatDesc,SCatAdesc,SCatParent,STerminal from bluecode_soqak.CATEGORIES";
            string query = @"select CategoryId,SCatDesc,SCatAdesc,SCatParent,STerminal from Categories";
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


        [Route("api/CATEGORIES/{SCatParent?}")]
        [HttpGet("{SCatParent}")]

        public JsonResult Get(int SCatParent)
        {
            string query = @"select CategoryId,SCatDesc,SCatAdesc,SCatParent,STerminal from Categories where SCatParent=@S_CAT_PARENT order by CategoryId";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("DefaultConnection");
            MySqlDataReader myReader;
            using (MySqlConnection myCon = new MySqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@S_CAT_PARENT", SCatParent);
                    /*myCommand.Parameters.AddWithValue("@usr_pass", usr_pass);*/
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);

        }
     
          


    }
}
