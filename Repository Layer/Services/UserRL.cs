using Database_Layer;
using Microsoft.Extensions.Configuration;
using Microsoft.IdentityModel.Tokens;
using MySqlX.XDevAPI.Common;
using Repository_Layer.FundooNotesContext;
using Repository_Layer.UserInterface;
using System;
using System.Collections.Generic;
using System.IdentityModel.Tokens.Jwt;
using System.Linq;
using System.Security.Claims;
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
            catch (Exception ex)
            {
                throw ex;
            }
        }
        public string LoginUser(string email, string password)
        {
            try
            {
                var result = fundooContext.Users.FirstOrDefault(u => u.email == email && u.password == password);
                if (result == null)
                {
                    return null;
                }
                return email;
                // string password = password;

            }
            catch (Exception ex)
            {

                throw ex;
            }
        }

        // Generate JWT token
        public static string GetJWTToken(string email, string userID)
        {
            var tokenHandler = new JwtSecurityTokenHandler();
            var tokenKey = Encoding.ASCII.GetBytes("THIS_IS_MY_KEY_TO_GENERATE_TOKEN");
            var tokenDescriptor = new SecurityTokenDescriptor
            {
                Subject = new ClaimsIdentity(new Claim[]
                {
                    new Claim("email", email),
                    new Claim("userID",userID.ToString())
                }),
                Expires = DateTime.UtcNow.AddHours(1),

                SigningCredentials =
                new SigningCredentials(
                    new SymmetricSecurityKey(tokenKey),
                    SecurityAlgorithms.HmacSha256Signature)
            };
            var token = tokenHandler.CreateToken(tokenDescriptor);
            return tokenHandler.WriteToken(token);
        }
    }
}
