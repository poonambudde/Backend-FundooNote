using Database_Layer;
using Repository_Layer.FundooNotesContext;
using Repository_Layer.UserInterface;
using System;
using System.Collections.Generic;
using System.Text;

namespace Repository_Layer.User
{
    public class UserRL : IUserRL
    {
        FundooContext fundooContext;
        public UserRL(FundooContext fundooContext)
        {
            this.fundooContext = fundooContext;
            
        }
        public void AddUser(UserPostModel user)
        {
            try
            {
                Entity.User userEntity = new Entity.User();
                userEntity.userID = new Entity.User().userID;
                userEntity.firstName = user.firstName;
                userEntity.lastName = user.lastName;
                userEntity.email = user.email;
                userEntity.password = user.password;
                userEntity.registeredDate = DateTime.Now;
                userEntity.address = user.address;
                fundooContext.Users.Add(userEntity);
                fundooContext.SaveChanges();
            }
            catch(Exception ex)
            {
                throw ex;
            }
        
        }
    }
}
