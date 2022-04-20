﻿using Business_Layer.Interfaces;
using Database_Layer;
using Microsoft.AspNetCore.Http;
using Microsoft.AspNetCore.Mvc;
using Repository_Layer.FundooNotesContext;
using System;
using System.Collections.Generic;
using System.Linq;
using System.Threading.Tasks;

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
                var getUserdata = fundooContext.Users.FirstOrDefault(u => u.email == user.email);
                if (getUserdata != null)
                {
                    return this.Ok(new { success = false, message = $"{user.email} is Already Exists" });
                }
                this.userBL.AddUser(user); 
                return this.Ok(new { success = true, message = $"Registration Successful {user.email}" });
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}