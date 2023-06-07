using Microsoft.AspNetCore.Hosting;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.FileProviders;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class GetImagesController : ControllerBase
    {

        private readonly IWebHostEnvironment _host;
        public GetImagesController(IWebHostEnvironment host)
        {
            _host = host;
        }


        [HttpGet("{images}/{cat}/{dd}")]

        //[Route("api/Upload/{cat?}/{dd?}/{i?}")]
        //[HttpPost("{cat}/{dd}/{i}")]



        //public async Task<List<byte[]>> Get([FromQuery] string images)
        public async Task<List<byte[]>> Get([FromRoute] string filename,int cat,int dd)
        //public async Task<IActionResult> Get([FromRoute] string filename)
        //public async Task<FileResult> Get([FromRoute] string filename)
        // public async Task<IActionResult> Get([FromRoute] string filename)
        {
           // images = "C:\\images\\1";

            List<byte[]> imageBytes = new List<byte[]>();

            //retrieve the content of folder
            var provider = new PhysicalFileProvider("D:\\");

            var ssbpth = "";

            if (cat == 1)
            {
                ssbpth = "images\\Car\\";
            }
            else if (cat == 2)
            {
                ssbpth = "images\\Estate\\";
            }
            else if (cat == 3)
            {
                ssbpth = "images\\Estate\\";
            }
            else if (cat == 4)
            {
                ssbpth = "images\\Product\\";
            }
            else if (cat == 5)
            {
                ssbpth = "images\\Accessories\\";
            }
            else if (cat == 6)
            {
                ssbpth = "images\\Industrial\\";
            }
            else if (cat == 7)
            {
                ssbpth = "images\\Service\\";
            }
            else if (cat == 8)
            {
                ssbpth = "images\\Job\\";
            }
            else if (cat == 9)
            {
                ssbpth = "images\\Animal\\";
            }


            var contents = provider.GetDirectoryContents(Path.Combine(ssbpth, dd.ToString()));
            //var contents = provider.GetDirectoryContents(Path.Combine("images\\Job\\", "21"));
            var objFiles = contents.OrderBy(m => m.LastModified);

            //ImageList = new List<string>();
            foreach (var item in objFiles.ToList())
            {
                String filePath = Path.Combine("D:\\", ssbpth + dd, item.Name  );
                byte[] bytes = System.IO.File.ReadAllBytes(filePath);
                
                imageBytes.Add(bytes);
                //ImageList.Add(item.);
                //ImageList.Add(item.Name);
            }

            //String filePath2 = Path.Combine("C:\\", "images\\1", "1.jpg");
            //byte[] b = System.IO.File.ReadAllBytes(filePath2);


            //String[] strArray = images.Split(',');
            //for (int i = 0; i < strArray.Length; i++)
            //{
            //    //String filePath = Path.Combine(_host.ContentRootPath, "images", strArray[i] + ".jpg");
            //    String filePath = Path.Combine("C:\\images\\", "images", strArray[i] + ".jpg");
            //    byte[] bytes = System.IO.File.ReadAllBytes(filePath);
            //    imageBytes.Add(bytes);




            //var imageFileStream = System.IO.File.OpenRead ("C:\\images\\1\\");
            //return File(imageFileStream, "image/jpeg");
            //}
            //return File(imageBytes, "image/png");
            return  imageBytes;
            //return System.IO.File(imageBytes, "image/jpg", "test");
            //return File(imageBytes, "image/png");
        }



    }
}
