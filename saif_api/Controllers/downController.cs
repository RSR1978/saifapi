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
    public class downController : ControllerBase
    {
        [BindProperty]
        public List<string> ImageList { get; set; }

        [HttpGet]
        public IActionResult Get()
        {
            var image = System.IO.File.OpenRead("C:\\images\\951fd366-7984-49c1-bbc4-0789eee4f1ed.jpg");
            return File(image, "image/jpg");
            //Byte[] b;


          


            //var image = System.IO.File.OpenRead("C:\\images\\");
            //return File(image, "image/jpg");

            //var provider = new PhysicalFileProvider("C:\\");
            //var contents = provider.GetDirectoryContents(Path.Combine("images", "1"));
            //var objFiles = contents.OrderBy(m => m.LastModified);

            //foreach (var item in objFiles)
            //{
            //ImageList.Add(new FileInfo(item.Name.ToString() ));
            //b = System.IO.File.OpenRead(item.PhysicalPath);
            //ImageList.Add(item);
            //}

            //b = await System.IO.File.ReadAllBytesAsync(Path.Combine(_environment.ContentRootPath, "Images", $"{imageName}"));
            //return File(b, "image/jpeg");






            //retrieve the content of folder
            //var provider = new PhysicalFileProvider("C:\\");
            //var contents = provider.GetDirectoryContents(Path.Combine("images", "1"));
            //var objFiles = contents.OrderBy(m => m.LastModified);

            //ImageList = new List<string>();
            //foreach (var item in objFiles.ToList())
            //{
            //ImageList.Add(item.);
            //ImageList.Add(item.Name);
            //}
            //return "ok";
            // return new JsonResult(objFiles);





        }

    }
}
