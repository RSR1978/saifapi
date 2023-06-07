using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;
using System.Data;
using System.Data.SqlClient;
using Microsoft.Extensions.Configuration;
using saif_api.Models;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class taskController : ControllerBase
    {

        private readonly IConfiguration _configuration;

        public taskController(IConfiguration configuration)
        {
            _configuration = configuration;
        }
        [HttpGet]

        public JsonResult Get()
        {
            string query = @"select tsk_id,dbo.Getopername(oper_no) oper_no,convert(varchar,tsk_date,105) tsk_date,dbo.GetClient(oper_hos) oper_hos,dbo.GetClient(oper_doc) oper_doc,tsk_note from task_info,Operation_Detail where task_info.tsk_oper_ser=Operation_Detail.oper_ser";

            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaifAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand=new SqlCommand(query,myCon))
                {
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                }
            }

            return new JsonResult(table);
        }


        [HttpPost]

        public JsonResult post(tasks opd)
        {
            string query = @"insert into task_info
                                values (@tsk_date,@tsk_note,@tsk_usr,@tsk_to,@tsk_to_dept,@tsk_active,@tsk_comp_dt,@tsk_usr_comp,@tsk_oper_ser)";
            /*(oper_no,oper_date,oper_hos,oper_doc,oper_user,oper_status,oper_note)*/
            DataTable table = new DataTable();
            string sqlDataSource = _configuration.GetConnectionString("SaifAppCon");
            SqlDataReader myReader;
            using (SqlConnection myCon = new SqlConnection(sqlDataSource))
            {
                myCon.Open();
                using (SqlCommand myCommand = new SqlCommand(query, myCon))
                {
                    /*myCommand.Parameters.AddWithValue("@oper_ser", opd.oper_ser);*/
                    /*myCommand.Parameters.AddWithValue("@oper_no", opd.oper_no);*/
                    myCommand.Parameters.AddWithValue("@tsk_date", opd.tsk_date);
                    myCommand.Parameters.AddWithValue("@tsk_note", opd.tsk_note);
                    myCommand.Parameters.AddWithValue("@tsk_usr", opd.tsk_usr);
                    myCommand.Parameters.AddWithValue("@tsk_to", opd.tsk_to);
                    myCommand.Parameters.AddWithValue("@tsk_to_dept", opd.tsk_to_dept);
                    myCommand.Parameters.AddWithValue("@tsk_active", opd.tsk_active);
                    myCommand.Parameters.AddWithValue("@tsk_comp_dt", DBNull.Value );
                    myCommand.Parameters.AddWithValue("@tsk_usr_comp", opd.tsk_usr_comp);
                    myCommand.Parameters.AddWithValue("@tsk_oper_ser", opd.tsk_oper_ser);
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }
            }

            return new JsonResult("Added Successfully");

        }


    }
}
