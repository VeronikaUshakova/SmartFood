using System;
using System.Collections.Generic;
using System.Security.Cryptography;
using System.Text;

namespace SmartFood.DAL.Entities
{
    public class Admin
    {
        public string Login;
        public string Password;

        public Admin(string login, string password)
        {
            this.Login = login;
            this.Password = password;
        }

        public static Admin admin = new Admin("2a4409796b6f78797ed4def9bcc9a033", "91788418b7c3540f525e2274fc245314");
    }
}
