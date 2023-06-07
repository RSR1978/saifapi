using Microsoft.AspNetCore.Authorization;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Identity;
using Microsoft.AspNetCore.Mvc;
using Microsoft.EntityFrameworkCore;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using saif_api.Authentication;
using saif_api.Data;
using saif_api.Models;
using saif_api.StaticMethod;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
using System.Text;
using System.Threading.Tasks;

namespace saif_api.Controllers
{

    [Route("api/[controller]")]
    [ApiController]
    public class AuthenticationController : ControllerBase
    {
        private readonly UserManager<ApplicationUser> userManager;
        private readonly IConfiguration _configuration;
        private readonly ApplicationDbContext databaseContext;
        //private readonly IEmailService _emailService;
        


        public AuthenticationController(UserManager<ApplicationUser>userManager,IConfiguration configuration)
        {
            this.userManager = userManager;
            //this.roleManager = roleManager;
            _configuration = configuration;
            
        }

        [HttpPost]
        [Route("Register")]
        public async Task<IActionResult> Register([FromBody] RegisterMoel model)
        {
            var userExist = await userManager.FindByNameAsync(model.UserName);
            if (userExist != null)
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = "User already Exist" });
              ApplicationUser user = new ApplicationUser()
              {
                  Email = model.Email,
                  SecurityStamp = Guid.NewGuid().ToString(),
                  UserName = model.UserName,
                  EmailConfirmed = true
              };
            var result = await userManager.CreateAsync(user, model.Password);
            if (!result.Succeeded)
            {
                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = "User Creation Failed" });
            }
            return Ok(new Response { Status = "Success", Massage = "User Created Successfully" });


        }

        [HttpPost]
        [Route("Login")]
        public async Task<IActionResult> Login([FromBody] LoginModel model)
        {
            var user = await userManager.FindByNameAsync(model.UserName);
            if (user != null && await userManager.CheckPasswordAsync(user,model.Password))
            {
                var userRoles = await userManager.GetRolesAsync(user);
                var authClaims = new List<Claim>
                {
                    new Claim(ClaimTypes.Name,user.UserName),
                    new Claim(JwtRegisteredClaimNames.Jti, Guid.NewGuid().ToString()),
                };

                foreach (var userRole in userRoles)
                {
                    authClaims.Add(new Claim(ClaimTypes.Role, userRole));
                }
                var authSignKey = new SymmetricSecurityKey(Encoding.UTF8.GetBytes(_configuration["JWT:Key"]));

                    var token = new JwtSecurityToken(
                    issuer:_configuration["JWT:ValidIssuer"],
                    audience:_configuration["JWT:ValidAudience"],
                    expires: DateTime.Now.AddHours(3),
                    claims:authClaims,
                    signingCredentials: new SigningCredentials(authSignKey, SecurityAlgorithms.HmacSha256)
                );

                return Ok(new
                {
                    token = new JwtSecurityTokenHandler().WriteToken(token),
                    expiration = token.ValidTo,
                    User = user.Id,
                    Emal=user.Email,
                    phone=user.PhoneNumber
                });

            }
            return Unauthorized();

        }

        [HttpPost]
        [Route("change-password")]
        public async Task<IActionResult> ChangePassword([FromBody] ChangePassword model)
        {
            var user = await userManager.FindByNameAsync(model.Username);
            if (user == null)
                return StatusCode(StatusCodes.Status404NotFound, new Response { Status = "Error", Massage = "User Does not Exist" });

            if (string.Compare(model.NewPassword, model.ConfirmNewPassword) != 0)
                return StatusCode(StatusCodes.Status400BadRequest, new Response { Status = "Error", Massage = "The New Password and Confirm Password Does Not Match" });

            var result = await userManager.ChangePasswordAsync(user, model.CurrentPassword, model.NewPassword);
            if(!result.Succeeded)
            {
                var errors = new List<string>();

                foreach(var error in result.Errors)
                {
                    errors.Add(error.Description);
                }

                return StatusCode(StatusCodes.Status500InternalServerError, new Response { Status = "Error", Massage = string.Join(", ", errors)});
            }
            return Ok(new Response { Status = "Success", Massage = "Password successfully changed." });

        }


        [HttpPost]
        [AllowAnonymous]
        [Route("SendPasswordResetCode")]
        public async Task<IActionResult> SendPasswordResetCode(String email)

        {
            email="raed_refai@yahoo.com";
          
            if (string.IsNullOrEmpty(email))
            {
                return BadRequest("Email should not be null or empty");
            }
            var user = await userManager.FindByEmailAsync(email);

            var token = await userManager.GeneratePasswordResetTokenAsync(user);

            int otp = RandomNumberGeneartor.Generate(100000, 999999);

            var resetPassword = new ResetPassword()
            {
                Email = email,
                OTP = otp.ToString(),
                Token = token,
                UserId = user.Id,
                InsertDateTimeUTC = DateTime.UtcNow
            };

            await databaseContext.AddAsync(resetPassword);
            await databaseContext.SaveChangesAsync();

            await EmailSender.SendEmailAsync(email, "Rest Password OTP", "Hello"
                + email + "<br><br>Please find the reset password token below<br><br><br>"
                + otp + "<br><br>Thanks<br>oktests.com");
            
            return Ok("Token sent successfully in email");

            //    var user = await userManager.FindByEmailAsync(email);

            //    if (user!=null)
            //    {
            //        var token = await userManager.GeneratePasswordResetTokenAsync(user);
            //        var Link = Url.Action("ResetPassword","Authentication",new { token,email = user.Email},Request.Scheme);
            //        var message = new Message(new string[] { user.Email! },"Confirmation email Link",confirmationLink!);
            //        emailService.SendEmail(message);
            //    }
        }


        //[HttpPost]
        //[AllowAnonymous]
        //[Route("ResetPasswordd")]
        //public async Task<IActionResult> ResetPasswordd([FromBody] ResetPassword)
        //{
        //    if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
        //    {
        //        return BadRequest("Email & New Password should not be null or empty");
        //    }

        //    // Get Identity User details user user manager
        //    var user = await userManager.FindByNameAsync(email);

        //    // getting token from otp
        //    var resetPasswordDetails = await databaseContext.ResetPasswords
        //        .Where(rp => rp.OTP == otp && rp.UserId == user.Id)
        //        .OrderByDescending(rp => rp.InsertDateTimeUTC)
        //        .FirstOrDefaultAsync();

        //    // Verify if token is older than 15 minutes
        //    var expirationDateTimeUtc = resetPasswordDetails.InsertDateTimeUTC.AddMinutes(15);

        //    if (expirationDateTimeUtc < DateTime.UtcNow)
        //    {
        //        return BadRequest("OTP is expired, please generate the new OTP");
        //    }

        //    var res = await userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, newPassword);

        //    if (!res.Succeeded)
        //    {
        //        return BadRequest();
        //    }

        //    return Ok();
        //}





        [HttpPost]
        [AllowAnonymous]
        [Route("ResetPassword")]
        public async Task<IActionResult> ResetPassword(string email, string otp, string newPassword)
        {
            if (string.IsNullOrEmpty(email) || string.IsNullOrEmpty(newPassword))
            {
                return BadRequest("Email & New Password should not be null or empty");
            }

            // Get Identity User details user user manager
            var user = await userManager.FindByNameAsync(email);

            // getting token from otp
            var resetPasswordDetails = await databaseContext.ResetPasswords
                .Where(rp => rp.OTP == otp && rp.UserId == user.Id)
                .OrderByDescending(rp => rp.InsertDateTimeUTC)
                .FirstOrDefaultAsync();

            // Verify if token is older than 15 minutes
            var expirationDateTimeUtc = resetPasswordDetails.InsertDateTimeUTC.AddMinutes(15);

            if (expirationDateTimeUtc < DateTime.UtcNow)
            {
                return BadRequest("OTP is expired, please generate the new OTP");
            }

            var res = await userManager.ResetPasswordAsync(user, resetPasswordDetails.Token, newPassword);

            if (!res.Succeeded)
            {
                return BadRequest();
            }

            return Ok();
        }

    }
}
