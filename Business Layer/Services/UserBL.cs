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
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
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

        public string LoginUser(string email, string password)
        {
            try
            {
                return this.userRL.LoginUser(email, password);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
    }
}
