using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saif_api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class FileUploadController : ControllerBase
    {
        public static IWebHostEnvironment _webHostEnvironment;


        public FileUploadController(IWebHostEnvironment webHostEnvironment)
        {
            _webHostEnvironment = webHostEnvironment;
        }

        [HttpPost]
        [Route("upload")]
        public async Task<string> Upload([FromForm] uploadfile obj)
        {
            if (obj.files.Length > 0)
            {
                try
                {

                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Upload\\"))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Upload\\");
                    }
                    if (!Directory.Exists(_webHostEnvironment.WebRootPath + "\\Upload\\" + obj.Id))
                    {
                        Directory.CreateDirectory(_webHostEnvironment.WebRootPath + "\\Upload\\" + obj.Id);
                    }
                   
                       // using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Upload\\" + ff))
                        using (FileStream fileStream = System.IO.File.Create(_webHostEnvironment.WebRootPath + "\\Upload\\" + obj.files.FileName))
                        {
                            obj.files.CopyTo(fileStream);
                            fileStream.Flush();

                            return "\\Upload\\" + obj.files.FileName;
                        }
                   
                   
                }

                catch (Exception ex)
                {

                    return ex.Message.ToString();
                }

            }
            else
            {
                return "Upload Failed";
            }
        }
    }
}
