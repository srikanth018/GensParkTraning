using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace ConsoleTasksApp
{
    
    public class LoginValidation
    {
        private string correctUser = "Admin";
        private string correctPass = "pass";

        public void ValidateLogin(string username, string password)
        {
            if (username.ToLower() == correctUser.ToLower() && password.ToLower() == correctPass.ToLower())
            {
                Console.WriteLine("Login Successful");
            }
            else
            {
                Console.WriteLine("Invalid username or password");
            }
        }
    }
}
