using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Microsoft.AspNetCore.Identity;
using Warehouse_Manager.MVVM.Model;
using Warehouse_Manager.Services.Interfaces;

namespace Warehouse_Manager.Services.AuthenticationServices
{
    public class AuthenticationService : IAuthenticationService
    {
        public async Task<bool> Register(string email, string username, string password, string confirmPassword)
        {
            bool succes = false;
            if (password == confirmPassword)
            {
                IPasswordHasher<User> hasher = new PasswordHasher<User>();

                User user = new User()
                {
                    Username = username,
                    Email = email,
                };

                string hashed = hasher.HashPassword(user, password);

                user.PasswordHash = hashed;
            }

            return succes;
        }

        public async Task<User> Login(string username, string password)
        {
            var user = new User();
            return user;
        }
    }
}
