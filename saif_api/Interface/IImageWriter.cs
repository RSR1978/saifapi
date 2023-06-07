using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Interface
{
    public interface IImageWriter
    {
        Task<string> UploadImage(IFormFile file, int cat, int dd, int i);

        //Task<int> id(int id);
       
    }
}
