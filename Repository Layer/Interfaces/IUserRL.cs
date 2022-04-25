using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.UserInterface
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);
        public string LoginUser(string email, string password);
        public bool ForgotPassword(string email);
        public bool ChangePassword(string email, string password, string confirmPassword);
        List<Entity.User> GetAllUsers();
    }

}
