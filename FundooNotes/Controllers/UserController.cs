using Business_Layer.Interfaces;
using Database_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.FundooNotesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Security.Claims;

namespace FundooNotes.Controllers
{
    [ApiController]
    [Route("[controller]")]
    public class UserController : ControllerBase
    {
        IUserBL userBL;
        FundooContext fundooContext;
        public UserController (IUserBL userBL, FundooContext fundooContext)
        {
            this.userBL = userBL;
            this.fundooContext = fundooContext;
        }
        [HttpPost("register")]
        public ActionResult RegisterUser(UserPostModel user)
        {
            try
            {
                var getUserData = fundooContext.Users.FirstOrDefault(u => u.email == user.email);
                if (getUserData != null)
                {
                    return this.Ok(new { success = false, message = $"{user.email} is Already Exists" });
                }
                this.userBL.AddUser(user);
                return this.Ok(new { success = true, message = $"Registration Successfull { user.email}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
     
        [HttpPost("login/{email}/{password}")]
        public ActionResult LoginUser(string email, string password)
        {
            try
            {
                var result = fundooContext.Users.FirstOrDefault(u => u.email == email );
                if (result != null)
                {
                    return this.Ok(new { success = false, message = $"{email} LoginFailed" });
                }
                this.userBL.LoginUser(email, password);
                return this.Ok(new { success = true, message = $"Login Successfull { email}, Token = {result}" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPost("ForgotPassword/{email}")]
        public IActionResult ForgotPassword(string email)
        {
            try
            {
                var result = this.userBL.ForgotPassword(email);
                if (result != false)
                {
                    return this.Ok(new
                    {
                        success = true,
                        message = $"Mail Sent Successfully " +
                        $" token:  {result}"
                    });

                }
                return this.BadRequest(new { success = false, message = $"mail not sent" });
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        [HttpPut("ChangePassword/{email}")]
        public IActionResult ChangePassword(string password, string confirmpassword)
        {
            try
            {
                var email = User.FindFirst(ClaimTypes.Email).Value.ToString();
                var userid = User.Claims.FirstOrDefault(x => x.Type.ToString().Equals("userId", StringComparison.InvariantCultureIgnoreCase));
                int userId = Int32.Parse(userid.Value);
                bool res = userBL.ChangePassword(email, password, confirmpassword);

                if (!res)
                {
                    return this.BadRequest(new { success = false, message = "enter valid password" });

                }
                else
                {
                    return this.Ok(new { success = true, message = "reset password set successfully" });
                }
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
