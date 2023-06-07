using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class uploadmultiController : ControllerBase
    {

        public async Task<string> UploadFile()
        {
            return "ok";

        }
    }
}
