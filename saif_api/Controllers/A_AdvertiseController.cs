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
    public class A_AdvertiseController : Controller
    {
        private readonly IConfiguration _configuration;

        public A_AdvertiseController(IConfiguration configuration)
        {
            _configuration = configuration;
        }

        [Route("api/A_Advertise/{ad_cat1?}/{ad_cat2?}")]
        [HttpGet("{ad_cat1}/{ad_cat2}")]

        public JsonResult Get(int ad_cat1, int ad_cat2)
        {
            string query = @"select ad_ser,ad_date,ad_price,ad_title,ad_loc,ad_fold_img,ad_space,ad_space_net,ad_room,ad_balcony,ad_age,ad_floor,ad_floor_no,ad_bath_no,ad_from,ad_tel,ad_furnished,ad_heating,ad_prop,ad_desc,ad_loc_at,ad_loc_long from A_Advertise where ad_cat1=@ad_cat1 and ad_cat2=@ad_cat2  order by ad_ser";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaifAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    myCommand.Parameters.AddWithValue("@ad_cat1", ad_cat1);
                    myCommand.Parameters.AddWithValue("@ad_cat2", ad_cat2);
                   
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);

        }


    }
}
