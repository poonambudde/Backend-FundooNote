using System;

namespace Database_Layer
{
    public class UserPostModel
    {
        public string firstName { get; set; }
        public string lastName { get; set; }
        public string email { get; set; }
        public DateTime registeredDate { get; set; }
        public string password { get; set; }
        public string address { get; set; }
    }
}