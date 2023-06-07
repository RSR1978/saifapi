using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class A_CategoryController : Controller
    {
        private readonly IConfiguration _configuration;

        public A_CategoryController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]
        [Route("api/A_CATEGORY")]
        public JsonResult Get()
        {
            string query = @"select A_CAT_SER,A_CAT_DESC,A_CAT_ADESC,A_PARENT from A_CATEGORY";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaifAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);



        }

        [Route("api/A_CATEGORY/{A_PARENT?}")]
        [HttpGet("{A_PARENT}")]

        public JsonResult Get(int A_PARENT)
        {
            string query = @"select A_CAT_SER,A_CAT_DESC,A_CAT_ADESC,A_PARENT from A_Category where A_PARENT=@A_PARENT order by A_CAT_SER";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaifAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {

                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@A_PARENT", A_PARENT);
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
