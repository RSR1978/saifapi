using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Microsoft.Extensions.Configuration;
using MySql.Data.MySqlClient;
using saif_api.Interface;
using System;
using System.Collections.Generic;
using System.Data;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Handler
{
    

    public interface IImageHandler
    {
        Task<IActionResult> UploadImage(IFormFile file, int cat, int dd,int i);
    }
    public class ImageHandler : IImageHandler
    {
        private readonly IImageWriter _imageWriter;
        
        public ImageHandler(IImageWriter imageWriter)
        {
            _imageWriter = imageWriter;
        }

        public async Task<IActionResult> UploadImage(IFormFile file, int cat, int dd,int i)
        {
            var result = await _imageWriter.UploadImage(file,  cat,  dd ,i);
            
            return new ObjectResult(result);
        }


    }
}
