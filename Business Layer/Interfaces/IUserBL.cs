using Database_Layer;
using Repository_Layer.Entity;
using System;
using System.Collections.Generic;
using System.Text;

namespace Business_Layer.Interfaces
{
    public interface IUserBL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser(string email, string password);
        public bool ForgotPassword(string email);
        public bool ResetPassword(string email, string password, string confirmpassword);
        List<User> GetAllUsers();
    }
}
