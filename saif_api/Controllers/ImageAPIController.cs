using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saif_api.Models;
using System;
using System.Collections.Generic;
using System.IO;
using System.Linq;
using System.Net;
using System.Net.Http;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class ImageAPIController : ControllerBase
    {
        [HttpPost]
        [Route("api/ImageAPI/GetImages")]
        //public HttpResponseMessage GetImages()
        public async Task<List<byte[]>> Get([FromQuery] string images)
        {
            //List<ImageModel> images = new List<ImageModel>();
            //Set the Image Folder Path.


            List<byte[]> imageBytes = new List<byte[]>();



            //string path = HttpContext.Current.Server.MapPath("~/Images/");
            string path = Path.Combine("D:\\", "images\\Job\\12\\");

            //Fetch the Image Files.
            foreach (string file in Directory.GetFiles(path))
            {
                //Read the Image as Byte Array.
                byte[] bytes = System.IO.File.ReadAllBytes(file);

                

                imageBytes.Add(bytes);


                //Convert and add Image as Base64 string.
               // images.Add(new ImageModel
               // {
                  //  Name = Path.GetFileName(file),
                 //   Data = Convert.ToBase64String(bytes, 0, bytes.Length)
               // });
            }
            //return File(imageBytes, "image/png");
            return imageBytes;
            // return new HttpResponseMessage(HttpStatusCode.Unauthorized, images);
        }



    }
}
