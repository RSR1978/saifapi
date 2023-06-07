using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using saif_api.Handler;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class UploadController : ControllerBase
    {
        private readonly IImageHandler _imageHandler;
        private readonly IConfiguration _configuration;
        public static IWebHostEnvironment _environment;
        public UploadController(IImageHandler imageHandler, IConfiguration configuration, IWebHostEnvironment environment)
        {
            _imageHandler = imageHandler;
            _configuration = configuration;
            _environment = environment;
        }

        private async Task<string> WriteFilenn(IFormFile file, string fnm, int cat,int dd,int i)
        {
            //string fileName;
            try
            {
                //Directory.CreateDirectory("D:\\images\\aliii");
                var pp="";
                    //= "D:\\images\\Job\\" + dd.ToString();


                if (cat == 1)
                {
                    pp = "D:\\images\\Car\\" + dd.ToString();
                }
                else if (cat == 2)
                {
                    pp = "D:\\images\\Estate\\" + dd.ToString();
                }
                else if (cat == 3)
                {
                    pp = "D:\\images\\Estate\\" + dd.ToString();
                }
                else if (cat == 4)
                {
                    pp = "D:\\images\\Product\\" + dd.ToString();
                }
                else if (cat == 5)
                {
                    pp = "D:\\images\\Accessories\\" + dd.ToString();
                }
                else if (cat == 6)
                {
                    pp = "D:\\images\\Industrial\\" + dd.ToString();
                }
                else if (cat == 7)
                {
                    pp = "D:\\images\\Service\\" + dd.ToString();
                }
                else if (cat == 8)
                {
                    pp = "D:\\images\\Job\\" + dd.ToString();
                }
                else if (cat == 9)
                {
                    pp = "D:\\images\\Animal\\" + dd.ToString();
                }
                

                //Directory.CreateDirectory(pp);
                bool folderExists = Directory.Exists(pp);
                if (!folderExists)
                    Directory.CreateDirectory(pp);
                //var extension = new StringBuilder(".")
                //    .Append(file.FileName.Split(".")[file.FileName.Split('.').Length - 1]);
                //fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                //var path = Path.Combine(Directory.GetCurrentDirectory(),
                //    "wwwroot\\images", fileName);
                var path = Path.Combine(
                  pp, fnm);
                using (var bits = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(bits);

                string query = @"insert into sooqak.adv_img
                                values (@id,@Adv_Id,@Adv_img,@comm)";
                //string query = @"update sooqak.jobadvertisments set Att=@att where Id=@dn";
                DataTable table = new DataTable();
                string MysqlDataSource = _configuration.GetConnectionString("DefaultConnection");
                MySqlDataReader myReader;
                using (MySqlConnection myCon = new MySqlConnection(MysqlDataSource))
                {
                    myCon.Open();
                    using (MySqlCommand myCommand = new MySqlCommand(query, myCon))
                    {
                        myCommand.Parameters.AddWithValue("@id", cat);
                        myCommand.Parameters.AddWithValue("@Adv_Id", i);
                        myCommand.Parameters.AddWithValue("@Adv_img", path);
                        myCommand.Parameters.AddWithValue("@comm", "test");
                        myReader = myCommand.ExecuteReader();
                        table.Load(myReader);
                        myReader.Close();
                        myCon.Close();
                    }

                }

            }
            catch (Exception e)
            {
                return e.Message;
            }
            return fnm;

        }

        [Route("api/Upload/{cat?}/{dd?}/{i?}")]
        [HttpPost("{cat}/{dd}/{i}")]

             
        //[HttpPost]
        public async Task<IActionResult> UploadImage(IFormFile file,int cat,int dd,int i)
        {

           


            FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + file.FileName);
            file.CopyTo(fileStream);


            //var pp = "D:\\images\\Job\\" + i.ToString();
            //Directory.CreateDirectory(pp);

            string ext;
            ext = Path.GetExtension(file.FileName);
            //(productAdvertisment.File.FileName.Split('.').Length - 1).ToString();
            string fileName;
            fileName = i.ToString() + ext;


           


            fileStream.Flush();
            var stream = file.OpenReadStream();
            // jobAdvertisment.m_img = ReadFully(stream);


           


            await WriteFilenn(file, fileName, cat,dd,i);




            return await _imageHandler.UploadImage(file,cat,dd,i);

          



        }

        //[HttpPost]
        //public async Task<IActionResult> rrr()
        //{
        //    return null;
        //}
    }
}
