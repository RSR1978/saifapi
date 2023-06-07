using Microsoft.AspNetCore.Mvc;
using Microsoft.AspNetCore.Mvc.Filters;
using saif_api.TokenAuthentication;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

namespace saif_api.Filter
{
    public class TokenAuthenticationFilter : Attribute, IAuthorizationFilter
    {
        public void OnAuthorization(AuthorizationFilterContext context)
        {
            var tokenManager = (ITokenManager)context.HttpContext.RequestServices.GetService(typeof(ITokenManager));
            var result = true;
            if (!context.HttpContext.Request.Headers.ContainsKey("Authorization"))
                result = false;
            string token = string.Empty;

            //if (result)
            //{
              //  token = context.HttpContext.Request.Headers.First(x => x.Key == "Authorization").Value;
              //  if (!tokenManager.verifytoken(token))
              //      result = false; 
            //}
            if (!result)
            {
                context.ModelState.AddModelError("Unauthorized", "You are Not Authorized.");
                context.Result = new UnauthorizedObjectResult(context.ModelState);
            }
        }
      

    }
}
