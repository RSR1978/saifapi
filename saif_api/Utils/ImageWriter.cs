using Microsoft.AspNetCore.Http;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using saif_api.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.IO;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace saif_api.Utils
{
    public class ImageWriter : IImageWriter
    {
        private readonly IConfiguration _configuration;

        public ImageWriter(IConfiguration configuration)
        {

            _configuration = configuration;
        }
        public async Task<string> UploadImage(IFormFile file, int cat, int dd, int i )
        {
            if (CheckIfImageFile(file))
            {
                return await WriteFile(file,  cat,  dd);
            }
            return "Invalid image file";
        }


        private async Task<string> WriteFile(IFormFile file, int cat, int dd)
        {
            string fileName;
            try 
            {
                var extension = new StringBuilder(".")
                    .Append(file.FileName.Split(".")[file.FileName.Split('.').Length - 1]);
                fileName = new StringBuilder(Guid.NewGuid().ToString()).Append(extension).ToString();
                //var path = Path.Combine(Directory.GetCurrentDirectory(),
                //    "wwwroot\\images", fileName);
                //string gg = "D:\\images\\" + cat + "\\" + dd + "\\" ;
                string gg = "D:\\images";
                //var path = Path.Combine(
                //  "c:\\images", fileName);

                var path = Path.Combine(
                 gg, fileName);

                using (var bits = new FileStream(path, FileMode.Create))
                    await file.CopyToAsync(bits);

                await WriteFile( path, cat, dd);
            }
            catch(Exception e)
            {
                return e.Message;
            }
            return fileName;
        }

        private async Task<string> WriteFile(string fnm,int cat, long dd)
        {
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
                    myCommand.Parameters.AddWithValue("@id", 8);
                    myCommand.Parameters.AddWithValue("@Adv_Id", 11);
                    myCommand.Parameters.AddWithValue("@Adv_img", "D:\\images\\");
                    myCommand.Parameters.AddWithValue("@comm", "test");
                    myReader = myCommand.ExecuteReader();
                    table.Load(myReader);
                    myReader.Close();
                    myCon.Close();
                }

            }

            return fnm;
        }


        private bool CheckIfImageFile(IFormFile file)
        {
            byte[] fileBytes;
            using(var ms=new MemoryStream())
            {
                file.CopyTo(ms);
                fileBytes = ms.ToArray();
            }
            return WriteHelper.GetImageFormat(fileBytes) != WriteHelper.ImageFormat.unknown;
        }
    }
}
