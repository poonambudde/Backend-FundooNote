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
        public UserController(IUserBL userBL, FundooContext fundooContext)
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
                    return this.BadRequest(new { success = false, message = $"{user.email} is Already Exists" });
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
                var result = this.userBL.LoginUser(email, password);
                if (result != null)
                {
                    return this.Ok(new { success = true, message = $"Login Successfull {result}" });
                }
                return this.BadRequest(new { success = false, message = $"{result} LoginFailed" });
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

        [HttpGet("getallusers")]
        public ActionResult GetAllUsers()
        {
            try
            {
                var result = userBL.GetAllUsers();
                return this.Ok(new { success = true, message = $"Below are the User data", data = result });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
        
        [HttpGet("GetAllUsersRedis")]
        public ActionResult GetAllUsers_ByRadisCache()
        {
            try
            {
                string serializeUserList;
                var userList = new List<User>();
                var redisUserList = distributedCache.Get(keyName);
                if (redisUserList != null)
                {
                    serializeUserList = Encoding.UTF8.GetString(redisUserList);
                    userList = JsonConvert.DeserializeObject<List<User>>(serializeUserList);
                }
                else
                {
                    userList = this.userBL.GetAllUsers();
                    serializeUserList = JsonConvert.SerializeObject(userList);
                    redisUserList = Encoding.UTF8.GetBytes(serializeUserList);
                    var option = new DistributedCacheEntryOptions().SetSlidingExpiration(TimeSpan.FromMinutes(20)).SetAbsoluteExpiration(TimeSpan.FromHours(6));
                    distributedCache.SetAsync(keyName, redisUserList, option);
                }
                return this.Ok(new { success = true, message = "Get Users successful!!!", data = userList });
            }
            catch (Exception e)
            {
                throw e;
            }
        }
    }
}
