using Business_Layer.Interfaces;
using Database_Layer;
using Repository_Layer.Entity;
using Repository_Layer.User;
using Repository_Layer.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Services
{
    public class UserBL : IUserBL
    {
        IUserRL userRL;
        public UserBL(IUserRL userRL)
        {
            this.userRL = userRL;
        }
        
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

        public bool ForgotPassword(string email)
        {
            try
            {
                return this.userRL.ForgotPassword(email);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public bool ChangePassword(string email, string password, string confirmPassword)
        {
            try
            {
                return userRL.ChangePassword(email, password, confirmPassword);
            }
            catch (Exception ex)
            {
                throw ex;
            }
        }

        public List<User> GetAllUsers()
        {
            try
            {
                return userRL.GetAllUsers();

            }
            catch (Exception e)
            {
                throw e;
            }
        }

    }
}
