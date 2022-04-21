using Database_Layer;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.UserInterface
{
    public interface IUserRL
    {
        public void AddUser(UserPostModel user);
        string LoginUser(string email, string password);
    }

}
