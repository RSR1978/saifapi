using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using saif_api.TokenAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Controllers
{
    [Route("api/[controller]")]
    [ApiController]
    public class authenticateController : ControllerBase
    {
        private readonly ITokenManager tokenManager;

        public authenticateController(ITokenManager tokenManager)
        {
            this.tokenManager = tokenManager;
        }
        public IActionResult Authenticate(String usr,string pwd)
        {
            

            if(tokenManager.Authenticate(usr,pwd))
            {
                return Ok(new { token = tokenManager.NewToken() });
            }
            else
            {
                ModelState.AddModelError("Unauthorized", "You are not Authorized");
                return Unauthorized(ModelState);
            }

        }


    }
}
