using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;

namespace PrizivaNet
{
    public class User
    {
        public string Email { get; set; }
        public string Password { get; set; }
        public string Role { get; set; }
        public string Login { get; set; }

        public User(string email, string password, string role, string login)
        {
            Email = email;
            Password = password;
            Role = role;
            Login = login;
        }
    }
}
