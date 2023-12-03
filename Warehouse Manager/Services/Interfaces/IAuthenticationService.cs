using System;
using System.Collections.Generic;
using System.Linq;
using System.Text;
using System.Threading.Tasks;
using Warehouse_Manager.MVVM.Model;

namespace Warehouse_Manager.Services.Interfaces
{
    public interface IAuthenticationService
    {
        Task<bool> Register(string email, string username, string password, string confirmPassword);
        Task<User> Login(string username, string password);
    }
}
