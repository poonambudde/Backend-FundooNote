using Database_Layer;
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
        public bool ChangePassword(string email, string password, string confirmpassword);
    }
}
