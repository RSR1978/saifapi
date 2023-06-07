using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;

// For more information on enabling Web API for empty projects, visit https://go.microsoft.com/fwlink/?LinkID=397860

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UserController : ControllerBase
    {


        private readonly IConfiguration _configuration;

        public UserController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [Route("api/user/{usr_name?}")]
        [HttpGet("{usr_name}/{usr_pass}")]

        public JsonResult Get(string usr_name,string usr_pass)
        {
            string query = @"select usr_id,usr_grp,usr_empn,usr_email from user_info where usr_name=@usr_name and usr_pass=@usr_pass";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaifAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@usr_name", usr_name);
                    myCommand.Parameters.AddWithValue("@usr_pass", usr_pass);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);

        }
        [HttpGet]
        public JsonResult Get()
        {
            string query = @"select usr_id,usr_name,usr_active,usr_grp,usr_empn,usr_email from user_info";

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

        // GET: api/<UserController>
        // [HttpGet]
        // public IEnumerable<string> Get()
        //{
        //  return new string[] { "value1", "value2" };
        // }

        // GET api/<UserController>/5
        //[HttpGet("{id}")]
        //public string Get(int id)
        //{
        //  return "usr_name";
        // }

        // POST api/<UserController>
        //[HttpPost]
        //public void Post([FromBody] string value)
        //{
        //}

        // PUT api/<UserController>/5
        //[HttpPut("{id}")]
        //public void Put(int id, [FromBody] string value)
        //{
        //}

        // DELETE api/<UserController>/5
        //[HttpDelete("{id}")]
        //public void Delete(int id)
        //{
        //}
    }
}
