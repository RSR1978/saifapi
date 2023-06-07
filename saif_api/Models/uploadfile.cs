using Microsoft.AspNetCore.Http;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Models
{
    public class uploadfile
    {
        public int Id { get; set; }

        public IFormFile files { get; set; }
    }
}
