using System;

namespace hw3webapp.Models
{
    public class User
    {
        public string username { get; set; }
        public string password  { get; set; }
        public Guid userID  { get; set; }
        public bool isActive { get; set; }

        public User(string username, string password)
        {
            this.username = username;
            this.password = password;
            userID = new Guid();
            isActive = false;
        }
        
        public User(){}
    }
}