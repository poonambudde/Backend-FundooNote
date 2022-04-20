using Business_Layer.Interfaces;
using Database_Layer;
using Repository_Layer.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public void AddUser(UserPostModel user)
        {
            try
            {
                this.userRL.AddUser(user);
            }
            catch(Exception ex)
            {
                throw ex;
            }
        }
    }
}

