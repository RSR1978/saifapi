using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageUploadController : ControllerBase
    {
        public static IWebHostEnvironment _environment;


        public ImageUploadController(IWebHostEnvironment environment,int id)
        {
            _environment = environment;
        }
        public class FileUpload
        {
            public IFormFile files { get; set; }
            public int id { get; set; }
        }

        [HttpPost]
        public async Task<string> Post([FromForm] FileUpload objFile,int id)
        {
           
                if (objFile.files.Length > 0)
                {
                   try
                   {

                      if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\"))
                      {
                          Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\");
                      }
                    if (!Directory.Exists(_environment.WebRootPath + "\\Upload\\" + id))
                    {
                        Directory.CreateDirectory(_environment.WebRootPath + "\\Upload\\" + id);
                    }
                    using (FileStream fileStream = System.IO.File.Create(_environment.WebRootPath + "\\Upload\\" + objFile.files.FileName))
                      {
                          objFile.files.CopyTo(fileStream);
                          fileStream.Flush();

                          return "\\Upload\\" + objFile.files.FileName;
                      }
                   }
                
                 catch (Exception ex)
                {

                    return ex.Message.ToString();
                }
            }
           else
                {
                    return "Failed";
                }
        }



    }
}
